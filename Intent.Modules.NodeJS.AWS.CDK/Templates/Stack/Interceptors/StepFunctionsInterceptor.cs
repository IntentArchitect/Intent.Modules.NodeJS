using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intent.Metadata.Models;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Builder;
using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack.Interceptors
{
    internal class StepFunctionsInterceptor : IStackTemplateInterceptor
    {
        private readonly StackTemplate _template;
        private readonly List<StateMachineDetails> _stateMachines = new();
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
                constructor.Class.File.AddImports(new [] { "Condition", "StateMachine", "StateMachineProps", "WaitTime" }, "aws-cdk-lib/aws-stepfunctions");
                constructor.Class.File.AddImport("aws_stepfunctions", "sfn", "aws-cdk-lib");
                constructor.Class.File.AddDefaultImport("StateMachineBuilder", "@andybalham/state-machine-builder-v2"); // TODO JL: Find out why this is causing SF exceptions (only happens which file contains existing which is not default)
            }

            foreach (var resource in resources)
            {
                var variableName = $"{resource.Name.RemoveSuffix("State", "Machine").ToCamelCase()}StateMachine";
                var getDefinitionMethodName = $"get{resource.Name.ToPascalCase()}Definition";

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

                _stateMachines.Add(new StateMachineDetails
                {
                    Resource = resource,
                    GetDefinitionMethodName = getDefinitionMethodName
                });
            }
        }

        public void ApplyPost(TypescriptConstructor constructor)
        {
            foreach (var stateMachine in _stateMachines)
            {
                var sb = new StringBuilder();
                sb.AppendLine("return new StateMachineBuilder()");

                var start = stateMachine.Resource.ChildElements.SingleOrDefault(x => x.SpecializationType == Constants.ElementName.StateMachineStart);
                if (start == null)
                {
                    throw new Exception($"Could not find \"Start\" state for \"{stateMachine.Resource.Name}\" [{stateMachine.Resource.Id}]");
                }

                const string indentation = $"{Tab}{Tab}{Tab}";
                BuildStep(sb, indentation, GetNextStep(start), new HashSet<IElement>());

                sb.Length -= Environment.NewLine.Length;
                sb.Append(';');

                constructor.Class.AddMethod(
                    name: stateMachine.GetDefinitionMethodName,
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
                        BuildLambdaInvoke(sb, indentation, element, alreadySeen);
                        break;
                    case Constants.ElementName.StateMachineChoice:
                        BuildChoice(sb, indentation, element, alreadySeen);
                        break;
                    case Constants.ElementName.StateMachineFail:
                        BuildFail(sb, indentation, element, alreadySeen);
                        break;
                    case Constants.ElementName.StateMachineEnd:
                        sb.AppendLine($"{indentation}.end()");
                        return;
                    default:
                        BuildStep(sb, indentation, GetNextStep(element), alreadySeen);
                        //throw new Exception($"Unknown step type: {element.SpecializationType}");
                        break;
                }

                element = GetNextStep(element);
            }
        }

        private void BuildFail(StringBuilder sb, string indentation, IElement element, ISet<IElement> alreadySeen)
        {
            sb.AppendLine($"{indentation}.fail('{element.Name}')");
        }

        private void BuildLambdaInvoke(StringBuilder sb, string indentation, IElement element, ISet<IElement> alreadySeen)
        {
            var lambdaStatement = _template.TypescriptFile.Classes.Single().Constructors.Single().Statements
                .SingleOrDefault(x => x.TryGetMetadata<IElement>(Constants.MetadataKey.SourceElement, out var source) && source.Id == element.TypeReference.Element.Id);
            if (lambdaStatement == null)
            {
                throw new Exception($"Could not find generated lambda statement for \"{element.Name}\", ensure you have the lambda modules installed.");
            }

            if (!lambdaStatement.TryGetMetadata<string>(Constants.MetadataKey.VariableName, out var variableName))
            {
                throw new Exception($"Could not resolve variable name for \"{element.Name}\"");
            }

            sb.AppendLine($"{indentation}.lambdaInvoke('{element.Name}', {{");
            sb.AppendLine($"{indentation}{Tab}lambdaFunction: this.{variableName},");
            sb.AppendLine($"{indentation}{Tab}outputPath: '$.Payload',"); // TODO JL: This should come from a stereotype property
            //sb.AppendLine($"{indentation}{Tab}retry: {{ maxAttempts: 3, backoffRate: 2, interval: Duration.seconds(1) }}"); // TODO JL: Should come from setting
            sb.AppendLine($"{indentation}}})");
        }

        private void BuildChoice(StringBuilder sb, string indentation, IElement element, ISet<IElement> alreadySeen)
        {
            sb.AppendLine($"{indentation}.choice('{element.Name}', {{");
            sb.AppendLine($"{indentation}{Tab}choices: [");

            var choices = element.AssociatedElements
                .Where(x => x.Association.SpecializationType == Constants.AssociationName.ChoiceTransition)
                .ToArray();
            var otherwise = GetNextStep(element);

            foreach (var choice in choices)
            {
                if (choice.Association.TargetEnd.TypeReference.Element is not IElement target)
                {
                    throw new Exception($"An association from \"{element.Name}\" [{element.Id}] does not have have a target end.");
                }

                // TODO JL:
                sb.AppendLine($"{indentation}{Tab}{Tab}{{ when: Condition.isPresent('TODO JL'), next: '{target.Name}' }},");
            }
            sb.AppendLine($"{indentation}{Tab}],");
            sb.AppendLine($"{indentation}{Tab}otherwise: '{otherwise?.Name ?? "UNKNOWN"}'");

            sb.AppendLine($"{indentation}}})");

            foreach (var choice in choices)
            {
                BuildStep(sb, indentation, choice.Association.TargetEnd.TypeReference.Element as IElement, alreadySeen);
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

        private class StateMachineDetails
        {
            public IElement Resource { get; set; }
            public string GetDefinitionMethodName { get; set; }
        }
    }
}
