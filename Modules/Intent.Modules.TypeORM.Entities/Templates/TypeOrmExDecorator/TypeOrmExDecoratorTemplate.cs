// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.TypeORM.Entities.Templates.TypeOrmExDecorator
{
    using System.Collections.Generic;
    using System.Linq;
    using Intent.Modules.Common;
    using Intent.Modules.Common.Templates;
    using Intent.Modules.Common.TypeScript.Templates;
    using Intent.Templates;
    using Intent.Metadata.Models;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.TypeORM.Entities\Templates\TypeOrmExDecorator\TypeOrmExDecoratorTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class TypeOrmExDecoratorTemplate : TypeScriptTemplateBase<object>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("import { SetMetadata } from \'@nestjs/common\';\r\n\r\nexport const TYPEORM_EX_CUSTOM_R" +
                    "EPOSITORY = \'TYPEORM_EX_CUSTOM_REPOSITORY\';\r\n\r\nexport function ");
            
            #line 14 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.TypeORM.Entities\Templates\TypeOrmExDecorator\TypeOrmExDecoratorTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("(entity: Function): ClassDecorator {\r\n  return SetMetadata(TYPEORM_EX_CUSTOM_REPO" +
                    "SITORY, entity);\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}