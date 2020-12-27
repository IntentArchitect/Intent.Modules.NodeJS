using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Templates.EntityTemplate
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class EntityTemplate : TypeScriptTemplateBase<Intent.Modelers.Domain.Api.ClassModel>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.TypeORM.Entities.EntityTemplate";

        public EntityTemplate(IOutputTarget outputTarget, ClassModel model) : base(TemplateId, outputTarget, model)
        {
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.Name}",
                fileName: $"{Model.Name.ToKebabCase()}.entity"
            );
        }

        private string GetBaseClassName()
        {
            return GetTypeName(EntityBaseTemplate.EntityBaseTemplate.TemplateId);
        }

        private string GetAttributeDecorators(AttributeModel model)
        {
            var hasNonDefaultSettings = false;
            var settings = new List<string>();
            if (model.TypeReference.IsNullable)
            {
                hasNonDefaultSettings = true;
                settings.Add("isNullable: true");
            }

            return $@"@Column({(hasNonDefaultSettings ? $"{{ {string.Join(", ", settings)} }}" : "")})";
        }

        private string GetAssociationDecorators(AssociationEndModel thatEnd)
        {
            if (!thatEnd.IsNavigable)
            {
                throw new InvalidOperationException("Cannot call this method if associationEnd is not navigable.");
            }
            var result = new StringBuilder();
            var sourceEnd = thatEnd.OtherEnd();
            if (!sourceEnd.IsCollection && !thatEnd.IsCollection) // one-to-one
            {
                result.Append($"@OneToOne(() => {GetTypeName(thatEnd)}{(sourceEnd.IsNavigable ? $", {thatEnd.Name} => {thatEnd.Name}.{sourceEnd.Name}" : "")})");
                if (thatEnd.IsTargetEnd())
                {
                    result.AppendLine($"@JoinColumn()");
                }
            }
            else if (!sourceEnd.IsCollection && thatEnd.IsCollection) // one-to-many
            {
                result.Append($"@OneToMany(() => {GetTypeName(thatEnd)}{(sourceEnd.IsNavigable ? $", {thatEnd.Name} => {thatEnd.Name}.{sourceEnd.Name}" : "")})");
            }
            else if (sourceEnd.IsCollection && !thatEnd.IsCollection) // many-to-one
            {
                result.Append($"@ManyToOne(() => {GetTypeName(thatEnd)}{(sourceEnd.IsNavigable ? $", {thatEnd.Name} => {thatEnd.Name}.{sourceEnd.Name}" : "")})");
            }
            else if (sourceEnd.IsCollection && thatEnd.IsCollection)
            {
                result.Append($"@ManyToMany(() => {GetTypeName(thatEnd)}{(sourceEnd.IsNavigable ? $", {thatEnd.Name} => {thatEnd.Name}.{sourceEnd.Name}" : "")})");
                if (thatEnd.IsTargetEnd())
                {
                    result.AppendLine($"@JoinTable()");
                }
            }
            return result.ToString();
        }
    }
}