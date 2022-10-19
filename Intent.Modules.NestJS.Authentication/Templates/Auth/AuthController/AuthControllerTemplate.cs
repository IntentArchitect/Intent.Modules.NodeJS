// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.NestJS.Authentication.Templates.Auth.AuthController
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
    
    #line 1 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth\AuthController\AuthControllerTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class AuthControllerTemplate : TypeScriptTemplateBase<object>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write(@"import { Controller, Get, Request, Post, UseGuards } from '@nestjs/common';
import { ApiBearerAuth, ApiBody, ApiProperty, ApiTags } from '@nestjs/swagger';

class UserCredentials {
  @ApiProperty({ example: 'john' })
  username: string;
  @ApiProperty({ example: 'changeme' })
  password: string;
}

@ApiTags('Auth')
@Controller()
export class ");
            
            #line 22 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth\AuthController\AuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(" {\r\n  constructor(private authService: ");
            
            #line 23 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth\AuthController\AuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetAuthServiceName()));
            
            #line default
            #line hidden
            this.Write(") {}\r\n\r\n  @UseGuards(");
            
            #line 25 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth\AuthController\AuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetLocalAuthGuardName()));
            
            #line default
            #line hidden
            this.Write(")\r\n  @Post(\'auth/login\')\r\n  @ApiBody({\r\n    description: \'User credentials\',\r\n   " +
                    " type: UserCredentials,\r\n  })\r\n  async login(@Request() req) {\r\n    return this." +
                    "authService.login(req.user);\r\n  }\r\n\r\n  @ApiBearerAuth()\r\n  @UseGuards(");
            
            #line 36 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.NestJS.Authentication\Templates\Auth\AuthController\AuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetJwtAuthGuardName()));
            
            #line default
            #line hidden
            this.Write(")\r\n  @Get(\'profile\')\r\n  getProfile(@Request() req) {\r\n    return req.user;\r\n  }\r\n" +
                    "}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
