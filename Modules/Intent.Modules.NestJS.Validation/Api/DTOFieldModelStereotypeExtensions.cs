using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.Api.ApiElementModelExtensions", Version = "1.0")]

namespace Intent.NodeJS.NestJS.Validation.Api
{
    public static class DTOFieldModelStereotypeExtensions
    {
        public static Validations GetValidations(this DTOFieldModel model)
        {
            var stereotype = model.GetStereotype(Validations.DefinitionId);
            return stereotype != null ? new Validations(stereotype) : null;
        }


        public static bool HasValidations(this DTOFieldModel model)
        {
            return model.HasStereotype(Validations.DefinitionId);
        }

        public static bool TryGetValidations(this DTOFieldModel model, out Validations stereotype)
        {
            if (!HasValidations(model))
            {
                stereotype = null;
                return false;
            }

            stereotype = new Validations(model.GetStereotype(Validations.DefinitionId));
            return true;
        }

        public class Validations
        {
            private IStereotype _stereotype;
            public const string DefinitionId = "2e7f99b2-0f21-4020-81a0-9ce74ef2bde3";

            public Validations(IStereotype stereotype)
            {
                _stereotype = stereotype;
            }

            public string Name => _stereotype.Name;

            public string Equals()
            {
                return _stereotype.GetProperty<string>("Equals");
            }

            public string NotEquals()
            {
                return _stereotype.GetProperty<string>("Not Equals");
            }

            public bool IsEmpty()
            {
                return _stereotype.GetProperty<bool>("Is Empty");
            }

            public bool IsNotEmpty()
            {
                return _stereotype.GetProperty<bool>("Is Not Empty");
            }

            public string IsIn()
            {
                return _stereotype.GetProperty<string>("Is In");
            }

            public string IsNotIn()
            {
                return _stereotype.GetProperty<string>("Is Not In");
            }

            public string IsNumberOptions()
            {
                return _stereotype.GetProperty<string>("Is Number Options");
            }

            public string IsDivisibleBy()
            {
                return _stereotype.GetProperty<string>("Is Divisible By");
            }

            public bool IsPositive()
            {
                return _stereotype.GetProperty<bool>("Is Positive");
            }

            public bool IsNegative()
            {
                return _stereotype.GetProperty<bool>("Is Negative");
            }

            public int? Min()
            {
                return _stereotype.GetProperty<int?>("Min");
            }

            public int? Max()
            {
                return _stereotype.GetProperty<int?>("Max");
            }

            public string MaxDate()
            {
                return _stereotype.GetProperty<string>("Max Date");
            }

            public string MinDate()
            {
                return _stereotype.GetProperty<string>("Min Date");
            }

            public string Contains()
            {
                return _stereotype.GetProperty<string>("Contains");
            }

            public string NotContains()
            {
                return _stereotype.GetProperty<string>("Not Contains");
            }

            public bool IsAlpha()
            {
                return _stereotype.GetProperty<bool>("Is Alpha");
            }

            public bool IsAlphanumeric()
            {
                return _stereotype.GetProperty<bool>("Is Alphanumeric");
            }

            public bool IsEmail()
            {
                return _stereotype.GetProperty<bool>("Is Email");
            }

            public string IsEmailOptions()
            {
                return _stereotype.GetProperty<string>("Is Email Options");
            }

            public bool IsLowercase()
            {
                return _stereotype.GetProperty<bool>("Is Lowercase");
            }

            public bool IsUppercase()
            {
                return _stereotype.GetProperty<bool>("Is Uppercase");
            }

            public int? MinLength()
            {
                return _stereotype.GetProperty<int?>("Min Length");
            }

            public int? MaxLength()
            {
                return _stereotype.GetProperty<int?>("Max Length");
            }

            public string Matches()
            {
                return _stereotype.GetProperty<string>("Matches");
            }

            public string ArrayContains()
            {
                return _stereotype.GetProperty<string>("Array Contains");
            }

            public string ArrayNotContains()
            {
                return _stereotype.GetProperty<string>("Array Not Contains");
            }

            public bool ArrayNotEmpty()
            {
                return _stereotype.GetProperty<bool>("Array Not Empty");
            }

            public int? ArrayMinSize()
            {
                return _stereotype.GetProperty<int?>("Array Min Size");
            }

            public int? ArrayMaxSize()
            {
                return _stereotype.GetProperty<int?>("Array Max Size");
            }

            public bool ArrayUnique()
            {
                return _stereotype.GetProperty<bool>("Array Unique");
            }

            public string IsInstance()
            {
                return _stereotype.GetProperty<string>("Is Instance");
            }

            public bool Allow()
            {
                return _stereotype.GetProperty<bool>("Allow");
            }

        }

    }
}