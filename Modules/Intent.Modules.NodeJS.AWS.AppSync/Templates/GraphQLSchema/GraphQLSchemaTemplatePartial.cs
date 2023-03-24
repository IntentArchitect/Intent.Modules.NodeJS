using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.AppSync.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.Types.Api;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.ProjectItemTemplate.Partial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.AppSync.Templates.GraphQLSchema
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class GraphQLSchemaTemplate : IntentTemplateBase<GraphQLEndpointModel>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.AppSync.GraphQLSchema";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public GraphQLSchemaTemplate(IOutputTarget outputTarget, GraphQLEndpointModel model) : base(TemplateId, outputTarget, model)
        {
        }

        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TemplateFileConfig(
                fileName: $"{Model.Name.ToCamelCase()}",
                fileExtension: "graphql",
                relativeLocation: this.GetSubstitutedRelativePath(Model.InternalElement.Package)
            );
        }

        private IEnumerable<Fragment> GetFragments()
        {
            var rootFragmentItems = new List<(Field Field, Fragment ReferencedType)>(2)
            {
                Model.QueryType != null
                    ? (
                        new Field("query", Model.QueryType.Name),
                        GetFragment(Model.QueryType.InternalElement, isInput: false))
                    // Fabricate an empty entry so as not to prevent `cdk synth` from failing with an error
                    : (
                        new Field("query", "Query"),
                        new Fragment(
                            directives: Enumerable.Empty<Directive>(),
                            type: "type",
                            name: "Query",
                            fields: Enumerable.Empty<Field>()))
            };

            if (Model.MutationType != null)
            {
                rootFragmentItems.Add((
                    new Field("mutation", Model.MutationType.Name),
                    GetFragment(Model.MutationType.InternalElement, isInput: false)));
            }

            yield return new Fragment(
                directives: Array.Empty<Directive>(),
                type: null,
                name: "schema",
                fields: rootFragmentItems.Select(x => x.Field));

            foreach (var (_, type) in rootFragmentItems)
            {
                yield return type;
            }

            foreach (var type in Model.Types.Select(x => x.InternalElement))
            {
                yield return GetFragment(type, isInput: false);
            }

            foreach (var type in GetInputTypes())
            {
                yield return GetFragment(type, isInput: true);
            }
        }

        private static Fragment GetFragment(IElement element, bool isInput)
        {
            var (type, name) = GetTypeAndName(element, isInput);

            return new Fragment(
                directives: Enumerable.Empty<Directive>(),
                type: type,
                name: name,
                fields: GetFields(element, isInput));
        }

        private static IEnumerable<Field> GetFields(IElement element, bool isInput)
        {
            return element.ChildElements
                .Where(field => field.SpecializationType is GraphQLSchemaFieldModel.SpecializationType or GraphQLMutationModel.SpecializationType)
                .Select(field => new Field(
                    name: field.Name,
                    type: GetSchemaType(field.TypeReference, isInput),
                    parameters: field.ChildElements
                        .Where(parameter => parameter.SpecializationType == GraphQLParameterModel.SpecializationType)
                        .Select(parameter => new Parameter(
                            name: parameter.Name,
                            type: GetSchemaType(parameter.TypeReference, isForInput: true)))));
        }

        private IEnumerable<IElement> GetInputTypes()
        {
            return Enumerable.Empty<GraphQLParameterModel>()
                .Concat(Model.QueryType?.Queries.SelectMany(x => x.Parameters) ?? Enumerable.Empty<GraphQLParameterModel>())
                .Concat(Model.MutationType?.Mutations.SelectMany(x => x.Parameters) ?? Enumerable.Empty<GraphQLParameterModel>())
                .SelectMany(x => Inputs(x.TypeReference.Element))
                .Distinct();

            static IEnumerable<IElement> Inputs(ICanBeReferencedType canBeReferencedType)
            {
                if (canBeReferencedType is not IElement element ||
                    !element.IsGraphQLSchemaTypeModel())
                {
                    yield break;
                }

                yield return element;

                foreach (var fieldSchemaTypeModel in element.ChildElements
                             .Where(x => x.SpecializationType == GraphQLSchemaFieldModel.SpecializationType)
                             .SelectMany(x => Inputs(x.TypeReference.Element)))
                {
                    yield return fieldSchemaTypeModel;
                }
            }
        }

        private static (string Type, string Name) GetTypeAndName(IElement element, bool isInput)
        {
            if (element.SpecializationType is GraphQLMutationTypeModel.SpecializationType
                or GraphQLQueryTypeModel.SpecializationType)
            {
                return ("type", element.Name);
            }

            var strippedName = element.Name.RemoveSuffix("Input", "Type");

            return isInput
                ? (Type: "input", Name: $"{strippedName}Input")
                : (Type: "type", Name: $"{strippedName}Type");
        }

        private static string GetSchemaType(ITypeReference typeReference, bool isForInput = false)
        {
            const string unknown = "UNKNOWN";

            var element = typeReference.Element as IElement;
            var typeName = element?.SpecializationType switch
            {
                GraphQLSchemaTypeModel.SpecializationType => $"{element.Name.RemoveSuffix("Input", "Type")}{(isForInput ? "Input" : "Type")}",
                GraphQLQueryTypeModel.SpecializationType => element.Name,
                GraphQLMutationTypeModel.SpecializationType => element.Name,
                TypeDefinitionModel.SpecializationType => element.Name switch
                {
                    "binary" => "String",
                    "bool" => "Boolean",
                    "byte" => "Int",
                    "char" => "String",
                    "date" => "String",
                    "datetime" => "String",
                    "datetimeoffset" => "String",
                    "decimal" => "Float",
                    "double" => "Float",
                    "float" => "Float",
                    "guid" => "String",
                    "int" => "Int",
                    "long" => "Float",
                    "short" => "Int",
                    "string" => "String",
                    _ => unknown
                },
                _ => unknown
            };

            if (typeReference.IsCollection)
            {
                typeName = $"[{typeName}]";
            }

            if (!typeReference.IsNullable)
            {
                typeName = $"{typeName}!";
            }

            return typeName;
        }

        private class Fragment
        {
            public Fragment(IEnumerable<Directive> directives, string type, string name, IEnumerable<Field> fields)
            {
                Directives = directives;
                Type = type;
                Name = name;
                Fields = fields;
            }

            public IEnumerable<Directive> Directives { get; }
            public string Type { get; }
            public string Name { get; }
            public IEnumerable<Field> Fields { get; }
        }

        private class Field
        {
            public Field(
                string name,
                string type,
                IEnumerable<Parameter> parameters = null,
                IEnumerable<Directive> directives = null)
            {
                Name = name;
                Type = type;
                Parameters = parameters ?? Enumerable.Empty<Parameter>();
                Directives = directives ?? Enumerable.Empty<Directive>();
            }

            public string Name { get; }
            public string Type { get; }
            public IEnumerable<Directive> Directives { get; }
            public IEnumerable<Parameter> Parameters { get; }
        }

        private class Parameter
        {
            public Parameter(string name, string type, IEnumerable<Directive> directives = null)
            {
                Name = name;
                Type = type;
                Directives = directives ?? Enumerable.Empty<Directive>();
            }

            public string Name { get; }
            public string Type { get; }
            public IEnumerable<Directive> Directives { get; }
        }

        private class Directive
        {
            public Directive(string name, IEnumerable<Argument> arguments)
            {
                Name = name;
                Arguments = arguments;
            }

            public string Name { get; }
            public IEnumerable<Argument> Arguments { get; }
        }

        private class Argument
        {
            public Argument(string name, string value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; }
            public string Value { get; }
        }
    }
}