// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.NestJS.Authentication.Templates.Auth.Strategies.LocalStrategy
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
    
    #line 1 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth.Strategies\LocalStrategy\LocalStrategyTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class LocalStrategyTemplate : TypeScriptTemplateBase<object>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("import { Injectable, UnauthorizedException } from \'@nestjs/common\';\r\nimport { Pas" +
                    "sportStrategy } from \'@nestjs/passport\';\r\nimport { Strategy } from \'passport-loc" +
                    "al\';\r\n\r\n@Injectable()\r\nexport class ");
            
            #line 15 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth.Strategies\LocalStrategy\LocalStrategyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(" extends PassportStrategy(Strategy) {\r\n  constructor(private readonly authService" +
                    ": ");
            
            #line 16 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth.Strategies\LocalStrategy\LocalStrategyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetAuthServiceName()));
            
            #line default
            #line hidden
            this.Write(@") {
    super();
  }

  async validate(username: string, password: string): Promise<any> {
    const user = await this.authService.validateUser(username, password);
    if (!user) {
      throw new UnauthorizedException();
    }
    return user;
  }
}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
