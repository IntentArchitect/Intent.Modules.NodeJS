using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Metadata.WebApi.Api;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.Types.Api;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Controllers.Templates.DtoModel;
using Intent.Modules.NestJS.Core.Events;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;
using Intent.Utils;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NestJS.Controllers.Templates.Controller
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class
        ControllerTemplate : TypeScriptTemplateBase<Intent.Modelers.Services.Api.ServiceModel, ControllerDecorator>
    {
        [IntentManaged(Mode.Fully)] public const string TemplateId = "Intent.NodeJS.NestJS.Controllers.Controller";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public ControllerTemplate(IOutputTarget outputTarget, Intent.Modelers.Services.Api.ServiceModel model) : base(TemplateId, outputTarget, model)
        {
            AddTypeSource(DtoModelTemplate.TemplateId);
        }

        public string ServiceClassName => GetTypeName(Service.ServiceTemplate.TemplateId, Model);

        public string GetReturnType(OperationModel operation)
        {
            if (operation.ReturnType == null)
            {
                return "void";
            }

            if (operation.GetHttpSettings().ReturnTypeMediatype().IsApplicationJson()
                && (GetTypeInfo(operation.ReturnType).IsPrimitive || operation.ReturnType.HasStringType()))
            {
                return $"{this.GetJsonResponseName()}<{GetTypeName(operation.TypeReference)}>";
            }

            return GetTypeName(operation.ReturnType);
        }

        private string GetResultValue(OperationModel operation)
        {
            if (operation.GetHttpSettings().ReturnTypeMediatype().IsApplicationJson()
                && (GetTypeInfo(operation.ReturnType).IsPrimitive || operation.ReturnType.HasStringType()))
            {
                return $"new {this.GetJsonResponseName()}<{GetTypeName(operation.TypeReference)}>(result)";
            }

            return "result";
        }

        public string GetParameters(OperationModel operation)
        {
            return string.Join(", ", operation.Parameters.Select(p => $"{p.Name.ToCamelCase()}"));
        }

        public override void BeforeTemplateExecution()
        {
            base.BeforeTemplateExecution();

            foreach (var decorator in GetDecorators())
            {
                decorator.BeforeTemplateExecution();
            }

            ExecutionContext.EventDispatcher.Publish(new NestJsControllerCreatedEvent(null, TemplateId, Model.Id));
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.Name.RemoveSuffix("Service", "Controller")}Controller",
                fileName: $"{Model.Name.RemoveSuffix("Service", "Controller").ToKebabCase()}.controller",
                relativeLocation: this.GetFolderPath());
        }

        private IEnumerable<string> GetClassDecorators()
        {
            foreach (var decorator in GetDecorators().SelectMany(x => x.GetClassDecorators()))
            {
                yield return decorator;
            }

            yield return $"@{ImportType("ApiTags", "@nestjs/swagger")}('{Model.Name.RemoveSuffix("Service", "Controller")}')";
            yield return $"@Controller('{Model.GetHttpServiceSettings().Route() ?? $"api/{Model.Name.RemoveSuffix("Service", "Controller").ToLower()}"}')";
        }

        private IEnumerable<string> GetOperationDecorators(OperationModel operation)
        {
            foreach (var decorator in GetDecorators().SelectMany(x => x.GetOperationDecorators(operation)))
            {
                yield return decorator;
            }

            yield return GetHttpVerbAndPath(operation);

            var apiResponse = operation.ReturnType != null ? $"{GetTypeName((IElement)operation.TypeReference.Element)}" : null;
            
            if (apiResponse != null && GetTypeInfo(operation.ReturnType).IsPrimitive || operation.ReturnType.HasStringType())
            {
                apiResponse = apiResponse switch
                {
                    "binary" => "String",
                    "bool" => "Boolean",
                    "byte" => "Number",
                    "char" => "String",
                    "date" => "Date",
                    "datetime" => "Date",
                    "datetimeoffset" => "Date",
                    "decimal" => "Number",
                    "double" => "Number",
                    "float" => "Number",
                    "guid" => "String",
                    "int" => "Number",
                    "long" => "Number",
                    "short" => "Number",
                    "string" => "String",
                    _ => $"'{apiResponse}'"
                };
            }
            switch (GetHttpVerb(operation))
            {
                case HttpVerb.GET:
                    yield return $@"@{ImportType("ApiOkResponse", "@nestjs/swagger")}({{
    description: 'Result retrieved successfully.',
    type: {apiResponse},{(operation.ReturnType?.IsCollection == true ? $@"
    isArray: true," : "")}
  }})";
                    break;
                case HttpVerb.POST:
                    yield return $@"@{ImportType("ApiCreatedResponse", "@nestjs/swagger")}({{
    description: 'The record has been successfully created.',{(apiResponse != null ? $@"
    type: {apiResponse}," : "")}{(operation.ReturnType?.IsCollection == true ? $@"
    isArray: true," : "")}
  }})";
                    break;
                case HttpVerb.PUT:
                case HttpVerb.PATCH:
                    yield return $@"@{(operation.ReturnType != null ? ImportType("ApiOkResponse", "@nestjs/swagger") : ImportType("ApiNoContentResponse", "@nestjs/swagger"))}({{
    description: 'Successfully updated.',{(apiResponse != null ? $@"
    type: {apiResponse}," : "")}{(operation.ReturnType?.IsCollection == true ? $@"
    isArray: true," : "")}
  }})";
                    break;
                case HttpVerb.DELETE:
                    yield return $@"@{ImportType("ApiOkResponse", "@nestjs/swagger")}({{
    description: 'Successfully deleted.',{(apiResponse != null ? $@"
    type: {apiResponse}," : "")}{(operation.ReturnType?.IsCollection == true ? $@"
    isArray: true," : "")}
  }})";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (operation.Parameters.Any())
            {
                yield return $"@{ImportType("ApiBadRequestResponse", "@nestjs/swagger")}({{ description: 'Bad request.' }})";
            }
            if (IsOperationSecured(operation))
            {
                yield return $"@{ImportType("ApiUnauthorizedResponse", "@nestjs/swagger")}({{ description: 'Unauthorized request.' }})";
                yield return $"@{ImportType("ApiForbiddenResponse", "@nestjs/swagger")}({{ description: 'Forbidden request.' }})";
            }
            if (GetHttpVerb(operation) == HttpVerb.GET && operation.ReturnType?.IsCollection == false)
            {
                yield return $"@{ImportType("ApiNotFoundResponse", "@nestjs/swagger")}({{ description: 'Response not found.' }})";
            }
            //yield return @"[ProducesResponseType(StatusCodes.Status500InternalServerError)]";
        }

        private string GetOperationParameters(OperationModel operation)
        {
            var parameters = new List<string>();
            parameters.Add($"@{ImportType("Req", "@nestjs/common")}() req: {ImportType("Request", "@nestjs/common")}");
            var verb = GetHttpVerb(operation);
            switch (verb)
            {
                case HttpVerb.PATCH:
                case HttpVerb.POST:
                case HttpVerb.PUT:
                    parameters.AddRange(operation.Parameters.Select(x =>
                        $"{GetParameterBindingAttribute(operation, x)}{x.Name}: {GetTypeName(x.TypeReference)}"));
                    break;
                case HttpVerb.GET:
                case HttpVerb.DELETE:
                    if (operation.Parameters.Any(x =>
                            x.TypeReference.Element.SpecializationTypeId != TypeDefinitionModel.SpecializationTypeId &&
                            x.GetParameterSettings().Source().IsDefault()))
                    {
                        Logging.Log.Warning(
                            $@"Intent.NestJS.Controllers: [{Model.Name}.{operation.Name}] Passing objects into HTTP {GetHttpVerb(operation)} operations is not well supported by this module.
    We recommend using a POST or PUT verb");
                        // Log warning
                    }

                    parameters.AddRange(operation.Parameters.Select(x =>
                        $"{GetParameterBindingAttribute(operation, x)}{x.Name}: {GetTypeName(x.TypeReference)}"));
                    break;
                default:
                    throw new NotSupportedException($"{verb} not supported");
            }

            return string.Join(", ", parameters);
        }

        private string GetHttpVerbAndPath(OperationModel o)
        {
            var decoratorName = GetHttpVerb(o).ToString().ToLower().ToPascalCase();
            return $"@{ImportType(decoratorName, "@nestjs/common")}('{GetPath(o) ?? ""}')";
        }

        private string GetPath(OperationModel operation)
        {
            var path = operation.GetHttpSettings().Route();
            return !string.IsNullOrWhiteSpace(path) ? path.Replace("{", ":").Replace("}", "") : null;
        }

        private string GetParameterBindingAttribute(OperationModel operation, ParameterModel parameter)
        {
            if (!parameter.HasParameterSettings())
            {
                return string.Empty;
            }

            if (parameter.GetParameterSettings().Source().IsDefault())
            {
                if (parameter.TypeReference.Element.SpecializationTypeId == DTOModel.SpecializationTypeId)
                {
                    return $"@{ImportType("Body", "@nestjs/common")}() ";
                }

                if (operation.GetHttpSettings()?.Route() != null &&
                    (operation.GetHttpSettings().Route().Contains($"{{{parameter.Name}}}") ||
                     operation.GetHttpSettings().Route().Contains($":{parameter.Name}")))
                {
                    return $"@{ImportType("Param", "@nestjs/common")}('{parameter.Name}') ";
                }

                return $"@{ImportType("Query", "@nestjs/common")}('{parameter.Name}') ";
            }

            if (parameter.GetParameterSettings().Source().IsFromBody())
            {
                return $"@{ImportType("Body", "@nestjs/common")}() ";
            }

            if (parameter.GetParameterSettings().Source().IsFromHeader())
            {
                return $"@{ImportType("Headers", "@nestjs/common")}('{parameter.GetParameterSettings().HeaderName()}') ";
            }

            if (parameter.GetParameterSettings().Source().IsFromQuery())
            {
                return $"@{ImportType("Query", "@nestjs/common")}('{parameter.Name}') ";
            }

            if (parameter.GetParameterSettings().Source().IsFromRoute())
            {
                return $"@{ImportType("Param", "@nestjs/common")}('{parameter.Name}') ";
            }

            return string.Empty;
        }
        private bool IsControllerSecured()
        {
            return Model.HasSecured();// || ExecutionContext.Settings.GetAPISettings().DefaultAPISecurity().IsSecured();
        }

        private bool IsOperationSecured(OperationModel operation)
        {
            return (IsControllerSecured() || operation.HasSecured()) && !operation.HasUnsecured();
        }

        private HttpVerb GetHttpVerb(OperationModel operation)
        {
            var verb = operation.GetHttpSettings().Verb();
            return Enum.TryParse(verb.Value, out HttpVerb verbEnum) ? verbEnum : HttpVerb.POST;
        }

        public enum HttpVerb
        {
            DELETE,
            GET,
            PATCH,
            POST,
            PUT
        }
    }
}