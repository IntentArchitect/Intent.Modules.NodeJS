using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.Api.ApiElementModelExtensions", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Api
{
    public static class ClassModelStereotypeExtensions
    {
        public static Repository GetRepository(this ClassModel model)
        {
            var stereotype = model.GetStereotype("69b79e8d-4445-44de-915a-05e984f30ee0");
            return stereotype != null ? new Repository(stereotype) : null;
        }


        public static bool HasRepository(this ClassModel model)
        {
            return model.HasStereotype("69b79e8d-4445-44de-915a-05e984f30ee0");
        }

        public static bool TryGetRepository(this ClassModel model, out Repository stereotype)
        {
            if (!HasRepository(model))
            {
                stereotype = null;
                return false;
            }

            stereotype = new Repository(model.GetStereotype("69b79e8d-4445-44de-915a-05e984f30ee0"));
            return true;
        }

        public class Repository
        {
            private IStereotype _stereotype;

            public Repository(IStereotype stereotype)
            {
                _stereotype = stereotype;
            }

            public string Name => _stereotype.Name;

        }

    }
}