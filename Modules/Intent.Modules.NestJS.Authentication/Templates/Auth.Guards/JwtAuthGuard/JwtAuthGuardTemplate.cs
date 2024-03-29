// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.NestJS.Authentication.Templates.Auth.Guards.JwtAuthGuard
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
    
    #line 1 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth.Guards\JwtAuthGuard\JwtAuthGuardTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class JwtAuthGuardTemplate : TypeScriptTemplateBase<object>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("import { ExecutionContext, Injectable } from \'@nestjs/common\';\r\nimport { Reflecto" +
                    "r } from \'@nestjs/core\';\r\nimport { AuthGuard } from \'@nestjs/passport\';\r\nimport " +
                    "{ IS_PUBLIC_KEY } from \'./public.decorator\';\r\n\r\n@Injectable()\r\nexport class ");
            
            #line 16 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth.Guards\JwtAuthGuard\JwtAuthGuardTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(@" extends AuthGuard('jwt') {
  constructor(protected reflector: Reflector) {
    super();
  }

  async canActivate(context: ExecutionContext): Promise<boolean> {
    if (this.isPublic(context)) {
      return true;
    }

    return await (super.canActivate(context) as Promise<boolean>);
  }

  protected isPublic(context: ExecutionContext): boolean {
    const isPublic = this.reflector.getAllAndOverride<boolean>(IS_PUBLIC_KEY, [
      context.getHandler(),
      context.getClass(),
    ]);
    return isPublic;
  }
}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
