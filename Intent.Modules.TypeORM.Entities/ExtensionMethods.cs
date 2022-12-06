using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.RDBMS.Api;
using Intent.Modelers.Domain.Api;

namespace Intent.Modules.TypeORM.Entities
{
    public static class ExtensionMethods
    {
        public static (IReadOnlyList<AttributeModel> PrimaryKeys, ClassModel FromClass) GetPrimaryKeys(this ClassModel model)
        {
            while (model != null)
            {
                var primaryKeys = model.Attributes.Where(x => x.HasPrimaryKey()).ToArray();
                if (primaryKeys.Length > 0)
                {
                    return (primaryKeys, model);
                }

                model = model.ParentClass;
            }

            return (Array.Empty<AttributeModel>(), null);
        }
    }
}
