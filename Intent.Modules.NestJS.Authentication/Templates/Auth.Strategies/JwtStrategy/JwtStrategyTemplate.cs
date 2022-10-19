// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.NestJS.Authentication.Templates.Auth.Strategies.JwtStrategy
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
    
    #line 1 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth.Strategies\JwtStrategy\JwtStrategyTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class JwtStrategyTemplate : TypeScriptTemplateBase<object>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("import { Injectable } from \'@nestjs/common\';\r\nimport { PassportStrategy } from \'@" +
                    "nestjs/passport\';\r\nimport { ExtractJwt, Strategy } from \'passport-jwt\';\r\nimport " +
                    "{ ConfigService } from \'@nestjs/config\';\r\n\r\n@Injectable()\r\nexport class ");
            
            #line 16 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth.Strategies\JwtStrategy\JwtStrategyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(" extends PassportStrategy(Strategy) {\r\n  constructor(config: ConfigService) {\r\n  " +
                    "  super({\r\n      jwtFromRequest: ExtractJwt.fromAuthHeaderAsBearerToken(),\r\n    " +
                    "  ignoreExpiration: false,\r\n      secretOrKey: config.get(\'");
            
            #line 21 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth.Strategies\JwtStrategy\JwtStrategyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(EnvironmentVariables.JwtKey));
            
            #line default
            #line hidden
            this.Write("\'),\r\n    });\r\n  }\r\n\r\n  async validate(payload: any): Promise<");
            
            #line 25 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth.Strategies\JwtStrategy\JwtStrategyTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetUserName()));
            
            #line default
            #line hidden
            this.Write("> {\r\n    return {\r\n      userId: payload.userId,\r\n      username: payload.usernam" +
                    "e,\r\n      roles: payload.roles,\r\n    };\r\n  }\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
