using System.Collections.Generic;
using Intent.Modules.Common.Templates;
using Intent.Modules.NodeJS.AWS.CDK.Templates.ApplicationStack;
using Intent.Modules.NodeJS.AWS.CDK.Templates.ApplicationStackProps;
using Intent.Modules.NodeJS.AWS.CDK.Templates.EntryPoint;
using Intent.Modules.NodeJS.AWS.CDK.Templates.Stack;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateExtensions", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates
{
    public static class TemplateExtensions
    {
        public static string GetApplicationStackName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(ApplicationStackTemplate.TemplateId);
        }

        public static string GetApplicationStackPropsName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(ApplicationStackPropsTemplate.TemplateId);
        }
        public static string GetEntryPointName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(EntryPointTemplate.TemplateId);
        }
        public static string GetStackName<T>(this IntentTemplateBase<T> template)
where T : Intent.Modelers.AWS.CDK.Api.StackModel
        {
            return template.GetTypeName(StackTemplate.TemplateId, template.Model);
        }

        public static string GetStackName(this IntentTemplateBase template, Intent.Modelers.AWS.CDK.Api.StackModel model)
        {
            return template.GetTypeName(StackTemplate.TemplateId, model);
        }

    }
}