using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.AppSync.Api;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.FileTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.AppSync.Templates.GraphQLSchema
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class GraphQLSchemaTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            var sb = new StringBuilder();

            foreach (var fragment in GetFragments())
            {
                if (fragment.Type != null)
                {
                    sb.Append($"{fragment.Type} ");
                }

                sb.Append($"{fragment.Name}");
                foreach (var directive in fragment.Directives)
                {
                    sb.Append($"{Environment.NewLine}  {directive.Name}");
                    var arguments = string.Join(
                        separator: ",    ",
                        values: directive.Arguments.Select(x => $"{x.Name}: {x.Value}{Environment.NewLine}"));

                    if (arguments.Length > 0)
                    {
                        sb.AppendLine("(");
                        sb.Append(arguments);
                        sb.Append(')');
                    }
                }

                sb.AppendLine(" {");

                foreach (var field in fragment.Fields)
                {
                    sb.Append($"    {field.Name.ToCamelCase()}");

                    var parameters = string.Join(
                        separator: ", ",
                        values: field.Parameters.Select(x => $"{x.Name}: {x.Type}"));

                    if (parameters.Length > 0)
                    {
                        sb.Append($"({parameters})");
                    }

                    sb.Append($": {field.Type}");
                    foreach (var directive in field.Directives)
                    {
                        sb.Append(' ');
                        var arguments = string.Join(
                            separator: ", ",
                            values: directive.Arguments.Select(x => $"{x.Name}: {x.Value}"));

                        if (arguments.Length > 0)
                        {
                            sb.Append($"({arguments})");
                        }
                    }
                    sb.AppendLine();
                }
                sb.AppendLine("}");
                sb.AppendLine();
            }

            // Remove extra NewLine from the end:
            sb.Length -= Environment.NewLine.Length;

            return sb.ToString();
        }
    }
}