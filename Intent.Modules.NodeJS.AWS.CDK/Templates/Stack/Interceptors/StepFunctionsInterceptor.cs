using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intent.Metadata.Models;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Builder;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Utils;

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack.Interceptors
{
    internal class StepFunctionsInterceptor : IStackTemplateInterceptor
    {
        private readonly StackTemplate _template;
        private readonly Dictionary<IElement, string> _stateMachineMethodNamesByResource = new();
        private const string Tab = "    ";

        public StepFunctionsInterceptor(StackTemplate template)
        {
            _template = template;
            _template.AddDependency(new NpmPackageDependency("@andybalham/state-machine-builder-v2", "^2.0.3"));
        }

        public void ApplyInitial(TypescriptConstructor constructor)
        {
            var resources = _template.Model.UnderlyingPackage.GetChildElementsOfType(Constants.ElementName.StateMachine)
                .OrderBy(x => x.Name)
                .ToArray();

            if (resources.Length > 0)
            {
                constructor.Class.File.AddImports(new[] { "Condition", "StateMachine", "StateMachineProps", "WaitTime" }, "aws-cdk-lib/aws-stepfunctions");
                constructor.Class.File.AddImport("aws_stepfunctions", "sfn", "aws-cdk-lib");
                constructor.Class.File.AddDefaultImport("StateMachineBuilder", "@andybalham/state-machine-builder-v2"); // TODO JL: Find out why this is causing SF exceptions (only happens which file contains existing which is not default)
            }

            foreach (var resource in resources)
            {
                var variableName = $"{resource.Name.RemoveSuffix("State", "Machine").ToCamelCase()}StateMachine";
                var getDefinitionMethodName = $"get{resource.Name.ToPascalCase()}Definition";
                _stateMachineMethodNamesByResource.Add(resource, getDefinitionMethodName);

                //if (resource.ParentElement.SpecializationType == Constants.ElementName.StateMachine)
                //{
                //    continue;
                //}

                constructor.Class.AddField(variableName, "StateMachine", field => field.PrivateReadOnly());
                constructor.AddStatement($@"this.{variableName} = new StateMachine(this, '{resource.Name}', {{
            stateMachineName: '{variableName}',
            stateMachineType: sfn.StateMachineType.STANDARD,
            definition: this.{getDefinitionMethodName}().build(this),
        }});", statement => statement
                    .SeparatedFromPrevious()
                    .AddMetadata(Constants.MetadataKey.SourceElement, resource)
                    .AddMetadata(Constants.MetadataKey.VariableName, variableName)
                );
            }
        }

        public void ApplyPost(TypescriptConstructor constructor)
        {
            foreach (var (resource, getDefinitionMethodName) in _stateMachineMethodNamesByResource)
            {
                var sb = new StringBuilder();
                sb.AppendLine("return new StateMachineBuilder()");

                var start = resource.ChildElements.SingleOrDefault(x => x.SpecializationType == Constants.ElementName.StateMachineStart);
                if (start == null)
                {
                    throw new Exception($"Could not find \"Start\" state for \"{resource.Name}\" [{resource.Id}]");
                }

                const string indentation = $"{Tab}{Tab}{Tab}";
                BuildStep(sb, indentation, GetNextStep(start), new HashSet<IElement>());

                sb.Length -= Environment.NewLine.Length;
                sb.Append(';');

                constructor.Class.AddMethod(
                    name: getDefinitionMethodName,
                    returnType: "StateMachineBuilder",
                    configure: method => method.AddStatement(sb.ToString()));
            }
        }

        private void BuildStep(StringBuilder sb, string indentation, IElement element, ISet<IElement> alreadySeen)
        {
            while (true)
            {
                if (!alreadySeen.Add(element))
                {
                    return;
                }

                switch (element.SpecializationType)
                {
                    case Constants.ElementName.StateMachineLambdaInvoke:
                        BuildLambdaInvoke(sb, indentation, element);
                        break;
                    case Constants.ElementName.StateMachineChoice:
                        BuildChoice(sb, indentation, element, alreadySeen);
                        break;
                    case Constants.ElementName.StateMachineSuccess:
                        BuildSuccess(sb, indentation, element);
                        break;
                    case Constants.ElementName.StateMachineFail:
                        BuildFail(sb, indentation, element);
                        break;
                    case Constants.ElementName.StateMachineEnd:
                        sb.AppendLine($"{indentation}.end()");
                        return;
                    case Constants.ElementName.StateMachineWait:
                        BuildWait(sb, indentation, element);
                        break;
                    case Constants.ElementName.StateMachineSqsSendMessage:
                        BuildSqsSend(sb, indentation, element);
                        break;
                    case Constants.ElementName.StateMachineParallel:
                        BuildParallel(sb, indentation, element);
                        break;
                    default:
                        //BuildStep(sb, indentation, GetNextStep(element), alreadySeen);
                        //Logging.Log.Warning($"Unknown step type: {element.SpecializationType}");
                        //break;
                        throw new Exception($"Unknown step type: {element.SpecializationType}");
                }

                element = GetNextStep(element);
            }
        }

        private void BuildParallel(StringBuilder sb, string indentation, IElement element)
        {
            sb.AppendLine($"{indentation}.parallel('{element.Name}', {{");
            sb.AppendLine($"{indentation}{Tab}branches: [");
            foreach (var stateMachine in element.ChildElements.Where(x => x.SpecializationType == Constants.ElementName.StateMachine))
            {
                sb.AppendLine($"{indentation}{Tab}{Tab}this.{_stateMachineMethodNamesByResource[stateMachine]}(),");
            }
            sb.AppendLine($"{indentation}{Tab}]");
            sb.AppendLine($"{indentation}}})");
        }

        private void BuildSqsSend(StringBuilder sb, string indentation, IElement element)
        {
            var statement = _template.TypescriptFile.Classes.Single().Constructors.Single().Statements
                .SingleOrDefault(x => x.TryGetMetadata<IElement>(Constants.MetadataKey.SourceElement, out var source) && source.Id == element.TypeReference.Element.Id);
            if (statement == null)
            {
                throw new Exception($"Could not find generated statement for \"{element.Name}\", ensure you have the lambda modules installed.");
            }

            if (!statement.TryGetMetadata<string>(Constants.MetadataKey.VariableName, out var variableName))
            {
                throw new Exception($"Could not resolve variable name for \"{element.Name}\"");
            }

            sb.AppendLine($"{indentation}.perform(new {_template.ImportType("aws_stepfunctions_tasks", "aws-cdk-lib")}.SqsSendMessage(this, '{element.Name}', {{");
            sb.AppendLine($"{indentation}{Tab}{Tab}queue: this.{variableName},");
            sb.AppendLine($"{indentation}{Tab}{Tab}messageBody: {_template.ImportType("TaskInput", "aws-cdk-lib/aws-stepfunctions")}.fromText(''),");
            sb.AppendLine($"{indentation}{Tab}}})");

            var retryElement = element.ChildElements.SingleOrDefault(x => x.SpecializationType == Constants.ElementName.SqsSendMessageRetry);
            if (retryElement != null)
            {
                var options = new List<string>
                {
                    Errors(retryElement)
                };
                options.AddRange(GetRetryOptions(retryElement));

                sb.AppendLine($"{indentation}{Tab}.addRetry({{");
                foreach (var option in options)
                {
                    sb.AppendLine($"{indentation}{Tab}{Tab}{option},");
                }
                sb.AppendLine($"{indentation}{Tab}}})");
            }

            var catchElement = element.ChildElements.SingleOrDefault(x => x.SpecializationType == Constants.ElementName.SqsSendMessageCatch);
            if (catchElement != null)
            {
                var resultPath = catchElement.GetStereotypeProperty<string>(
                    Constants.Stereotype.SqsSendMessageCatchSettings.Name,
                    Constants.Stereotype.SqsSendMessageCatchSettings.Property.ResultPath);
                sb.AppendLine($"{indentation}{Tab}.addCatch(");
                sb.AppendLine($"{indentation}{Tab}{Tab}this.{_stateMachineMethodNamesByResource[(IElement)catchElement.TypeReference.Element]}().build(this),");
                sb.AppendLine($"{indentation}{Tab}{Tab}{{");
                sb.AppendLine($"{indentation}{Tab}{Tab}{Tab}{Errors(catchElement)},");
                sb.AppendLine($"{indentation}{Tab}{Tab}{Tab}resultPath: '{resultPath}'");
                sb.AppendLine($"{indentation}{Tab}{Tab}}}");
                sb.AppendLine($"{indentation}{Tab})");
            }

            sb.AppendLine($"{indentation})");

            static string Errors(IElement element)
            {
                var errors = element.ChildElements
                    .Where(x => x.SpecializationType == Constants.ElementName.SqsSendMessageError)
                    .Select(x => $"'{x.Name}'");

                return $"errors: [{string.Join(", ", errors)}]";
            }
        }

        private void BuildWait(StringBuilder sb, string indentation, IElement element)
        {
            var stereotype = element.GetStereotype(Constants.Stereotype.WaitSettings.Name);
            var waitType = stereotype.GetProperty<string>(Constants.Stereotype.WaitSettings.Property.DurationType);
            var waitAmount = stereotype.GetProperty<int>(Constants.Stereotype.WaitSettings.Property.DurationAmount);
            var duration = $"{_template.ImportType("Duration", "aws-cdk-lib")}.{waitType}({waitAmount:D})";

            sb.AppendLine(@$"{indentation}.wait('{element.Name}', {{");
            sb.AppendLine(@$"{indentation}{Tab}time: {_template.ImportType("WaitTime", "aws-cdk-lib/aws-stepfunctions")}.duration({duration})");
            sb.AppendLine(@$"{indentation}}})");
        }

        private static void BuildSuccess(StringBuilder sb, string indentation, IElement element)
        {
            sb.AppendLine($"{indentation}.pass('{element.Name}')");
        }

        private static void BuildFail(StringBuilder sb, string indentation, IElement element)
        {
            sb.AppendLine($"{indentation}.fail('{element.Name}')");
        }

        private void BuildLambdaInvoke(StringBuilder sb, string indentation, IElement element)
        {
            var statement = _template.TypescriptFile.Classes.Single().Constructors.Single().Statements
                .SingleOrDefault(x => x.TryGetMetadata<IElement>(Constants.MetadataKey.SourceElement, out var source) && source.Id == element.TypeReference.Element.Id);
            if (statement == null)
            {
                throw new Exception($"Could not find generated statement for \"{element.Name}\", ensure you have the lambda modules installed.");
            }

            if (!statement.TryGetMetadata<string>(Constants.MetadataKey.VariableName, out var variableName))
            {
                throw new Exception($"Could not resolve variable name for \"{element.Name}\"");
            }

            var settings = element.GetStereotype(Constants.Stereotype.LambdaInvokeSettings.Name);
            var outputPath = settings.GetProperty<string>(Constants.Stereotype.LambdaInvokeSettings.Property.OutputPath);

            sb.AppendLine($"{indentation}.lambdaInvoke('{element.Name}', {{");
            sb.AppendLine($"{indentation}{Tab}lambdaFunction: this.{variableName},");

            if (!string.IsNullOrWhiteSpace(outputPath))
            {
                sb.AppendLine($"{indentation}{Tab}outputPath: '{outputPath}',");
            }

            var retryOptions = GetRetryOptions(element);
            if (retryOptions.Count > 0)
            {
                sb.AppendLine($"{indentation}{Tab}retry: {{ {string.Join(", ", retryOptions)} }},");
            }

            sb.AppendLine($"{indentation}}})");
        }

        private IReadOnlyCollection<string> GetRetryOptions(IElement element)
        {
            var retrySettings = element.GetStereotype(Constants.Stereotype.RetrySettings.Name);
            var retryMaxAttempts = retrySettings.GetProperty<int?>(Constants.Stereotype.RetrySettings.Property.MaxAttempts);
            var retryBackOffRate = retrySettings.GetProperty<int?>(Constants.Stereotype.RetrySettings.Property.BackoffRate);
            var retryIntervalAmount = retrySettings.GetProperty<int?>(Constants.Stereotype.RetrySettings.Property.IntervalAmount);
            var retryIntervalType = retrySettings.GetProperty<string>(Constants.Stereotype.RetrySettings.Property.IntervalType);

            var retryOptions = new List<string>(3);
            if (retryMaxAttempts != null)
            {
                retryOptions.Add($"maxAttempts: {retryMaxAttempts:D}");
            }

            if (retryBackOffRate != null)
            {
                retryOptions.Add($"backoffRate: {retryBackOffRate:D}");
            }

            if (retryIntervalAmount != null)
            {
                retryOptions.Add($"interval: {_template.ImportType("Duration", "aws-cdk-lib")}.{retryIntervalType}({retryIntervalAmount:D})");
            }

            return retryOptions;
        }

        private void BuildChoice(StringBuilder sb, string indentation, IElement element, ISet<IElement> alreadySeen)
        {
            sb.AppendLine($"{indentation}.choice('{element.Name}', {{");
            sb.AppendLine($"{indentation}{Tab}choices: [");

            var choices = element.OwnedAssociations
                .Where(x => x.SpecializationType == Constants.AssociationName.RuleTransition)
                .ToArray();
            var otherwise = GetNextStep(element);

            foreach (var choice in choices)
            {
                if (choice.TargetEnd.TypeReference.Element is not IElement target)
                {
                    throw new Exception($"An association from \"{element.Name}\" [{element.Id}] does not have have a target end.");
                }

                var conditionalStatement = choice.TargetEnd.ChildElements.SingleOrDefault();
                var isCommentedOut = conditionalStatement == null;

                sb.AppendLine($"{indentation}{Tab}{Tab}{(isCommentedOut ? "// " : string.Empty)}{{ when: {GetCondition(conditionalStatement)}, next: '{target.Name}' }},");
            }
            sb.AppendLine($"{indentation}{Tab}],");
            sb.AppendLine($"{indentation}{Tab}otherwise: '{otherwise?.Name ?? "UNKNOWN"}'");

            sb.AppendLine($"{indentation}}})");

            foreach (var choice in choices)
            {
                BuildStep(sb, indentation, choice.TargetEnd.TypeReference.Element as IElement, alreadySeen);
            }
        }

        private string GetCondition(IElement element)
        {
            if (element == null)
            {
                return "UNKNOWN";
            }

            var condition = _template.ImportType("Condition", "aws-cdk-lib/aws-stepfunctions");

            var elementName = element.TypeReference.Element?.Name.ToLowerInvariant();
            if (elementName is "and" or "or")
            {
                return $"{condition}.{elementName}({string.Join(", ", element.ChildElements.Select(GetCondition))})";
            }

            var stereotype = element.GetStereotype(Constants.Stereotype.ConditionalStatementSettings.Name);
            var not = stereotype.GetProperty<bool>(Constants.Stereotype.ConditionalStatementSettings.Property.Not);
            var variable = stereotype.GetProperty<string>(Constants.Stereotype.ConditionalStatementSettings.Property.Variable);
            var @operator = stereotype.GetProperty<string>(Constants.Stereotype.ConditionalStatementSettings.Property.Operator);
            var type = stereotype.GetProperty<string>(Constants.Stereotype.ConditionalStatementSettings.Property.Type);
            var valueType = stereotype.GetProperty<string>(Constants.Stereotype.ConditionalStatementSettings.Property.ValueType);
            var value = stereotype.GetProperty<string>(Constants.Stereotype.ConditionalStatementSettings.Property.Value);
            var booleanValue = stereotype.GetProperty<bool>(Constants.Stereotype.ConditionalStatementSettings.Property.BooleanValue);

            return @operator switch
            {
                "is present" => $"{condition}.is{(not ? "Not" : string.Empty)}Present('{variable}')",
                "is of type" => IsOfType(),
                "is equal to" => ComparesTo("Equals"),
                "is less than" => ComparesTo("LessThan"),
                "is greater than" => ComparesTo("GreaterThan"),
                "is greater than or equal to" => ComparesTo("GreaterThanEquals"),
                "is less than or equal to" => ComparesTo("LessThanEquals"),
                "matches string" => $"{condition}.stringMatches('{variable}', '{value}')",
                _ => throw new ArgumentOutOfRangeException(nameof(@operator), "Out of range", @operator)
            };

            string IsOfType() => type switch
            {
                "Number" => $"{condition}.is{not}Numeric",
                "Timestamp" => $"{condition}.is{not}Timestamp",
                "Boolean" => $"{condition}.is{not}Boolean",
                "String" => $"{condition}.is{not}String",
                "Null" => $"{condition}.is{not}Null",
                _ => throw new ArgumentOutOfRangeException(nameof(type), "Out of range", type)
            };

            string ComparesTo(string comparisonType)
            {
                var (methodType, jsonPath, argument) = valueType switch
                {
                    "Number constant" => ("number", string.Empty, value),
                    "Number variable" => ("number", "JsonPath", value),
                    "String constant" => ("string", string.Empty, $"'{value}'"),
                    "String variable" => ("string", "JsonPath", $"'{value}'"),
                    "Timestamp constant" => ("timestamp", string.Empty, $"'{value}'"),
                    "Timestamp variable" => ("timestamp", "JsonPath", $"'{value}'"),
                    "Boolean constant" => ("boolean", string.Empty, booleanValue.ToString().ToLowerInvariant()),
                    "Boolean variable" => ("boolean", "JsonPath", booleanValue.ToString().ToLowerInvariant()),
                    _ => throw new ArgumentOutOfRangeException(nameof(value), "Out of range", value)
                };

                var result = $"{condition}.{methodType}{comparisonType}{jsonPath}('{variable}', {argument})";

                return not
                    ? $"{condition}.not({result})"
                    : result;
            }
        }

        private static IElement GetNextStep(IElement element)
        {
            var transitions = element.OwnedAssociations
                .Where(x => x.SpecializationType == Constants.AssociationName.StateTransition)
                .ToArray();

            var transition = transitions.Length switch
            {
                1 => transitions[0],
                _ => throw new Exception(
                    $"Expected single \"{Constants.AssociationName.StateTransition}\" association from \"{element.Name}\" [{element.Id}]")
            };

            return transition.TargetEnd.TypeReference.Element as IElement
                   ?? throw new Exception($"Association from \"{element.Name}\" [{element.Id}] does not have have a target end.");
        }
    }
}
