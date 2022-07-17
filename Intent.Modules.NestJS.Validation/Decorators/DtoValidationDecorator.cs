using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Types.Api;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Controllers.Templates.DtoModel;
using Intent.Modules.NestJS.Core.Events;
using Intent.NodeJS.NestJS.Validation.Api;
using Intent.RoslynWeaver.Attributes;
using Intent.Utils;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecorator", Version = "1.0")]

namespace Intent.Modules.NestJS.Validation.Decorators
{
    [IntentManaged(Mode.Merge)]
    public class DtoValidationDecorator : DtoModelDecorator
    {
        [IntentManaged(Mode.Fully)]
        public const string DecoratorId = "Intent.NodeJS.NestJS.Validation.DtoValidationDecorator";

        [IntentManaged(Mode.Fully)]
        private readonly DtoModelTemplate _template;
        [IntentManaged(Mode.Fully)]
        private readonly IApplication _application;

        private static Dictionary<string, TypeScriptTemplateBase<EnumModel>> _enumTemplatesById;
        private static readonly object LockObject = new();

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public DtoValidationDecorator(DtoModelTemplate template, IApplication application)
        {
            _template = template;
            _application = application;

            _template.AddDependency(Controllers.NpmPackageDependencies.ClassTransformer);
            _template.AddDependency(NpmPackageDependencies.ClassValidator);
        }

        public override void BeforeTemplateExecution()
        {
            if (_enumTemplatesById == null)
            {
                lock (LockObject)
                {
                    _enumTemplatesById ??= _application.OutputTargets
                        .SelectMany(outputTarget => outputTarget.TemplateInstances)
                        .OfType<TypeScriptTemplateBase<EnumModel>>()
                        .GroupBy(x => x.Model.Id)
                        .Where(x => x.Count() == 1)
                        .ToDictionary(x => x.Key, x => x.Single());
                }
            }

            _template.ExecutionContext.EventDispatcher.Publish(new NestApplicationGlobalPipeRequest(
                "new ValidationPipe()",
                new[]
                {
                    ("ValidationPipe", "@nestjs/common")
                }));
        }

        public override IEnumerable<string> GetDecorators(DTOFieldModel dtoField)
        {
            var numberTypes = new[] { "byte", "decimal", "double", "float", "int", "long", "short" };
            var dateTypes = new[] { "date", "datetime", "datetimeoffset" };

            if (!dtoField.TryGetValidations(out var validations))
            {
                var maxLength = GetTextConstraintsMaxLength(dtoField);
                if (maxLength.HasValue)
                {
                    yield return $"@{_template.ImportType("MaxLength", "class-validator")}({maxLength})";
                }

                yield break;
            }

            // Common validation decorators
            {
                if (!string.IsNullOrWhiteSpace(validations.Equals()))
                {
                    yield return $"@{_template.ImportType("Equals", "class-validator")}({validations.Equals()})";
                }

                if (!string.IsNullOrWhiteSpace(validations.NotEquals()))
                {
                    yield return $"@{_template.ImportType("NotEquals", "class-validator")}({validations.NotEquals()})";
                }

                if (validations.IsEmpty())
                {
                    yield return $"@{_template.ImportType("IsEmpty", "class-validator")}()";
                }

                if (validations.IsNotEmpty())
                {
                    yield return $"@{_template.ImportType("IsNotEmpty", "class-validator")}()";
                }

                if (!string.IsNullOrWhiteSpace(validations.IsIn()))
                {
                    yield return $"@{_template.ImportType("IsIn", "class-validator")}({validations.IsIn()})";
                }

                if (!string.IsNullOrWhiteSpace(validations.IsNotIn()))
                {
                    yield return $"@{_template.ImportType("IsNotIn", "class-validator")}({validations.IsNotIn()})";
                }
            }

            // Type validation decorators
            {
                if (!dtoField.TypeReference.IsCollection &&
                    dtoField.TypeReference.Element?.Name == "bool")
                {
                    yield return $"@{_template.ImportType("IsBoolean", "class-validator")}()";
                }

                if (!dtoField.TypeReference.IsCollection &&
                    dateTypes.Contains(dtoField.TypeReference.Element?.Name))
                {
                    yield return $"@{_template.ImportType("IsDate", "class-validator")}()";
                }

                if (!dtoField.TypeReference.IsCollection &&
                    dtoField.TypeReference.Element?.Name == "string")
                {
                    yield return $"@{_template.ImportType("IsString", "class-validator")}()";
                }

                if (!dtoField.TypeReference.IsCollection &&
                    numberTypes.Contains(dtoField.TypeReference.Element?.Name))
                {
                    yield return $"@{_template.ImportType("IsNumber", "class-validator")}({validations.IsNumberOptions()?.Trim()})";
                }

                if (!dtoField.TypeReference.IsCollection &&
                    dtoField.TypeReference.Element?.Name == "int")
                {
                    yield return $"@{_template.ImportType("IsInt", "class-validator")}()";
                }

                if (dtoField.TypeReference.IsCollection)
                {
                    yield return $"@{_template.ImportType("IsArray", "class-validator")}()";
                }

                if (!dtoField.TypeReference.IsCollection &&
                    dtoField.TypeReference.Element.IsEnumModel() &&
                    _enumTemplatesById.TryGetValue(dtoField.TypeReference.Element!.Id, out var enumTemplate))
                {
                    yield return $"@{_template.ImportType("IsEnum", "class-validator")}({_template.GetTypeName(enumTemplate)})";
                }
            }

            // Number validation decorators
            if (!dtoField.TypeReference.IsCollection &&
                numberTypes.Contains(dtoField.TypeReference.Element?.Name))
            {
                if (!string.IsNullOrWhiteSpace(validations.IsDivisibleBy()))
                {
                    yield return $"@{_template.ImportType("IsDivisibleBy", "class-validator")}({validations.IsDivisibleBy()})";
                }

                if (validations.IsPositive())
                {
                    yield return $"@{_template.ImportType("IsPositive", "class-validator")}()";
                }

                if (validations.IsNegative())
                {
                    yield return $"@{_template.ImportType("IsNegative", "class-validator")}()";
                }

                if (validations.Min() != null)
                {
                    yield return $"@{_template.ImportType("Min", "class-validator")}({validations.Min()})";
                }

                if (validations.Max() != null)
                {
                    yield return $"@{_template.ImportType("Max", "class-validator")}({validations.Max()})";
                }
            }

            // Date validation decorators
            if (!dtoField.TypeReference.IsCollection &&
                dateTypes.Contains(dtoField.TypeReference.Element?.Name))
            {
                if (validations.MinDate() != null)
                {
                    yield return $"@{_template.ImportType("MinDate", "class-validator")}({validations.MinDate()})";
                }

                if (validations.MaxDate() != null)
                {
                    yield return $"@{_template.ImportType("MaxDate", "class-validator")}({validations.MaxDate()})";
                }
            }

            // String validation decorators
            if (!dtoField.TypeReference.IsCollection &&
                dtoField.TypeReference.Element?.Name == "string")
            {
                if (!string.IsNullOrWhiteSpace(validations.Contains()))
                {
                    yield return $"@{_template.ImportType("Contains", "class-validator")}({validations.Contains()})";
                }

                if (!string.IsNullOrWhiteSpace(validations.NotContains()))
                {
                    yield return $"@{_template.ImportType("NotContains", "class-validator")}({validations.NotContains()})";
                }

                var minLength = validations.MinLength();
                var maxLength = validations.MaxLength() ?? GetTextConstraintsMaxLength(dtoField);

                if (minLength.HasValue && maxLength.HasValue)
                {
                    yield return $"@{_template.ImportType("Length", "class-validator")}({minLength}, {maxLength})";
                }
                else if (minLength.HasValue)
                {
                    yield return $"@{_template.ImportType("MinLength", "class-validator")}({minLength})";
                }
                else if (maxLength.HasValue)
                {
                    yield return $"@{_template.ImportType("MaxLength", "class-validator")}({maxLength})";
                }

                if (!string.IsNullOrWhiteSpace(validations.Matches()))
                {
                    yield return $"@{_template.ImportType("Matches", "class-validator")}({validations.Matches()})";
                }
            }

            // Array validation decorators
            if (dtoField.TypeReference.IsCollection)
            {
                if (!string.IsNullOrWhiteSpace(validations.ArrayContains()))
                {
                    yield return $"@{_template.ImportType("ArrayContains", "class-validator")}({validations.ArrayContains()})";
                }

                if (!string.IsNullOrWhiteSpace(validations.ArrayNotContains()))
                {
                    yield return $"@{_template.ImportType("ArrayNotContains", "class-validator")}({validations.ArrayNotContains()})";
                }

                if (validations.ArrayNotEmpty())
                {
                    yield return $"@{_template.ImportType("ArrayNotEmpty", "class-validator")}()";
                }

                if (validations.ArrayMinSize() != null)
                {
                    yield return $"@{_template.ImportType("ArrayMinSize", "class-validator")}({validations.ArrayMinSize()})";
                }

                if (validations.ArrayMaxSize() != null)
                {
                    yield return $"@{_template.ImportType("ArrayMaxSize", "class-validator")}({validations.ArrayMaxSize()})";
                }

                if (validations.ArrayUnique())
                {
                    yield return $"@{_template.ImportType("ArrayUnique", "class-validator")}()";
                }
            }

            // Object validation decorators
            {
                if (!string.IsNullOrWhiteSpace(validations.IsInstance()))
                {
                    yield return $"@{_template.ImportType("IsInstance", "class-validator")}({validations.IsInstance()})";
                }
            }

            // Other decorators
            {
                if (validations.Allow())
                {
                    yield return $"@{_template.ImportType("Allow", "class-validator")}()";
                }
            }

            foreach (var decorator in base.GetDecorators(dtoField))
            {
                yield return decorator;
            }
        }

        /// <summary>
        /// Checks to see if the field is mapped to an attribute, then checks for the presence of a
        /// <c>Text Constraints</c> stereotype on it, then if it has <c>MaxLength</c> property with
        /// a value and if all are true, then returns its value.
        /// </summary>
        private static int? GetTextConstraintsMaxLength(DTOFieldModel dtoField)
        {
            const string stereotypeName = "Text Constraints";
            const string stereotypePropertyName = "MaxLength";

            try
            {
                if (!dtoField.InternalElement.IsMapped ||
                    !dtoField.InternalElement.MappedElement.Element.IsAttributeModel())
                {
                    return null;
                }

                var attribute = dtoField.InternalElement.MappedElement.Element.AsAttributeModel();
                if (!attribute.HasStereotype(stereotypeName))
                {
                    return null;
                }

                var maxLength = attribute.GetStereotypeProperty<int?>(stereotypeName, stereotypePropertyName);

                return maxLength;
            }
            catch (Exception exception)
            {
                Logging.Log.Debug($"Exception occurred when attempting to resolve [{stereotypePropertyName}]" +
                                  $" for DTO-Field [{dtoField.Name}] for DTO [{dtoField.InternalElement.ParentElement?.Name}]:" +
                                  $"{Environment.NewLine}{exception}");
                return null;
            }
        }
    }
}