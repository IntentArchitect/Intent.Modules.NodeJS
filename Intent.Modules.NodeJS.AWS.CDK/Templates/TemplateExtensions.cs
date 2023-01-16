using System.Collections.Generic;
using Intent.Modules.Common.Templates;
using Intent.Modules.NodeJS.AWS.CDK.Templates.EntryPoint;
using Intent.Modules.NodeJS.AWS.CDK.Templates.Stack;
using Intent.Modules.NodeJS.AWS.CDK.Templates.StackBase;
using Intent.Modules.NodeJS.AWS.CDK.Templates.StackBaseProps;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateExtensions", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates
{
    public static class TemplateExtensions
    {
        public static string GetEntryPointName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(EntryPointTemplate.TemplateId);
        }
        public static string GetStackName<T>(this IntentTemplateBase<T> template)
where T : Intent.Modelers.AWS.Api.AWSPackageModel
        {
            return template.GetTypeName(StackTemplate.TemplateId, template.Model);
        }

        public static string GetStackName(this IntentTemplateBase template, Intent.Modelers.AWS.Api.AWSPackageModel model)
        {
            return template.GetTypeName(StackTemplate.TemplateId, model);
        }

        public static string GetStackBaseName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(StackBaseTemplate.TemplateId);
        }

        public static string GetStackBasePropsName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(StackBasePropsTemplate.TemplateId);
        }

    }
}