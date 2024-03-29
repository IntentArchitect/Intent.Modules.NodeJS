// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.NestJS.Authentication.Templates.Auth.AuthService
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
    
    #line 1 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth\AuthService\AuthServiceTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class AuthServiceTemplate : TypeScriptTemplateBase<object>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("import { Injectable } from \'@nestjs/common\';\r\nimport { JwtService } from \'@nestjs" +
                    "/jwt\';\r\n\r\n@Injectable()\r\nexport class ");
            
            #line 14 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth\AuthService\AuthServiceTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(" {\r\n  constructor(\r\n    private readonly usersService: ");
            
            #line 16 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth\AuthService\AuthServiceTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetUsersServiceName()));
            
            #line default
            #line hidden
            this.Write(",\r\n    private readonly jwtService: JwtService\r\n  ) {}\r\n\r\n  async validateUser(us" +
                    "ername: string, pass: string): Promise<");
            
            #line 20 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth\AuthService\AuthServiceTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetUserContextName()));
            
            #line default
            #line hidden
            this.Write("> {\r\n    const user = await this.usersService.findOne(username);\r\n    if (user &&" +
                    " user.password === pass) {\r\n      const { password, ...result } = user;\r\n      r" +
                    "eturn result;\r\n    }\r\n    return null;\r\n  }\r\n\r\n  async login(user: ");
            
            #line 29 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth\AuthService\AuthServiceTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetUserContextName()));
            
            #line default
            #line hidden
            this.Write("): Promise<{ access_token: string }> {\r\n    const payload = { sub: user.userId, ." +
                    "..user };\r\n    return {\r\n      access_token: this.jwtService.sign(payload),\r\n   " +
                    " };\r\n  }\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
