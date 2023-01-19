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
            var schemaFieldFragments = SchemaFieldFragments().ToArray();

            yield return new Fragment(
                name: "schema",
                type: null,
                fields: schemaFieldFragments
                    .Select(x => new Field(x.Name.ToLowerInvariant(), x.Name)),
                directives: Array.Empty<Directive>());

            foreach (var schemaFieldFragment in schemaFieldFragments)
            {
                yield return schemaFieldFragment;
            }

            foreach (var type in Model.Types)
            {
                yield return GetFragment(type, isInput: false);
            }

            foreach (var type in GetInputTypes())
            {
                yield return GetFragment(type, isInput: true);
            }
        }

        private static Fragment GetFragment(GraphQLSchemaTypeModel schemaType, bool isInput)
        {
            var (type, name) = GetTypeAndName(schemaType.Name, isInput);

            return new Fragment(
                directives: Enumerable.Empty<Directive>(),
                type: type,
                name: name,
                fields: schemaType.Fields.Select(x => new Field(x.Name, GetSchemaTypeName(x, isInput))));
        }

        private IEnumerable<Fragment> SchemaFieldFragments()
        {
            var queryFields = Model.QueryType.Queries
                .Select(q => new Field($"{q.Name}{Parameters(q.Parameters.Select(p => p.InternalElement))}",
                    GetSchemaTypeName(q)));
            yield return new Fragment(Enumerable.Empty<Directive>(), "type", "Query", queryFields);

            var mutationFields = Model.MutationType.Mutations
                .Select(m =>
                    new Field($"{m.Name}{Parameters(m.Parameters.Select(p => p.InternalElement), isForInput: true)}",
                        GetSchemaTypeName(m)));
            yield return new Fragment(Enumerable.Empty<Directive>(), "type", "Mutation", mutationFields);

            static string Parameters(IEnumerable<IElement> elements, bool isForInput = false)
            {
                var parameters = string.Join(", ", elements.Select(x => $"{x.Name}: {GetSchemaTypeName(x.TypeReference, isForInput)}"));

                return parameters.Length > 0
                    ? $"({parameters})"
                    : string.Empty;
            }
        }

        private IEnumerable<GraphQLSchemaTypeModel> GetInputTypes()
        {
            return Enumerable.Empty<GraphQLParameterModel>()
                .Concat(Model.QueryType.Queries.SelectMany(x => x.Parameters))
                .Concat(Model.MutationType.Mutations.SelectMany(x => x.Parameters))
                .SelectMany(x => Inputs(x.TypeReference.Element))
                .Distinct();

            static IEnumerable<GraphQLSchemaTypeModel> Inputs(ICanBeReferencedType canBeReferencedType)
            {
                if (canBeReferencedType is not IElement element ||
                    !element.IsGraphQLSchemaTypeModel())
                {
                    yield break;
                }

                var schemaTypeModel = element.AsGraphQLSchemaTypeModel();

                yield return schemaTypeModel;

                foreach (var fieldSchemaTypeModel in schemaTypeModel.Fields.SelectMany(x => Inputs(x.TypeReference.Element)))
                {
                    yield return fieldSchemaTypeModel;
                }
            }
        }

        private static (string Type, string Name) GetTypeAndName(string typeName, bool isInput)
        {
            var strippedName = typeName.RemoveSuffix("Input", "Type");

            return isInput
                ? (Type: "input", Name: $"{strippedName}Input")
                : (Type: "type", Name: $"{strippedName}Type");
        }

        private static string GetSchemaTypeName(IHasTypeReference hasTypeReference, bool isForInput = false)
            => GetSchemaTypeName(hasTypeReference.TypeReference, isForInput);

        private static string GetSchemaTypeName(ITypeReference typeReference, bool isForInput = false)
        {
            const string unknown = "UNKNOWN";

            var element = typeReference.Element as IElement;
            var typeName = element?.SpecializationType switch
            {
                GraphQLSchemaTypeModel.SpecializationType => $"{element.Name.RemoveSuffix("Input", "Type")}{(isForInput ? "Input" : "Type")}",
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
            public Field(string name, string type, IEnumerable<Directive> directives = null)
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