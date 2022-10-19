// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.TypeORM.Entities.Templates.OrmConfig
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
    
    #line 1 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.TypeORM.Entities\Templates\OrmConfig\OrmConfigTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class OrmConfigTemplate : TypeScriptTemplateBase<IList<Intent.Modelers.Domain.Api.ClassModel>>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write(@"import { TypeOrmModuleOptions } from '@nestjs/typeorm';
import { get } from 'env-var';
import { config } from 'dotenv';
config();

const commonConf = {
  ENTITIES: [__dirname + '/domain/entities/*.entity{.ts,.js}'],
  MIGRATIONS: [__dirname + '/migrations/**/*{.ts,.js}'],
  MIGRATIONS_RUN: get('DB_MIGRATIONS_RUN').asBool(),
  SYNCHRONIZE: get('DB_SYNCHRONIZE').asBool(),
};

const ");
            
            #line 22 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.TypeORM.Entities\Templates\OrmConfig\OrmConfigTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(": TypeOrmModuleOptions = {\r\n  name: \'default\',\r\n");
            
            #line 24 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.TypeORM.Entities\Templates\OrmConfig\OrmConfigTemplate.tt"
 foreach (var option in GetDatabaseProviderOptions()) { 
            
            #line default
            #line hidden
            this.Write("  ");
            
            #line 25 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.TypeORM.Entities\Templates\OrmConfig\OrmConfigTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(option));
            
            #line default
            #line hidden
            this.Write(",\r\n");
            
            #line 26 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.TypeORM.Entities\Templates\OrmConfig\OrmConfigTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"  logging: true,
  entities: commonConf.ENTITIES,
  migrations: commonConf.MIGRATIONS,
  migrationsRun: commonConf.MIGRATIONS_RUN,
  synchronize: commonConf.SYNCHRONIZE,
};

if (process.env.NODE_ENV === 'prod') {
  // your production options here
}

if (process.env.NODE_ENV === 'test') {
  // your test options here
}

export { ");
            
            #line 42 "C:\Dev\Intent.Modules.NodeJS\Intent.Modules.TypeORM.Entities\Templates\OrmConfig\OrmConfigTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(" };");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
