using System.Collections.Generic;

namespace Intent.Modules.NestJS.Core.Events
{
    public class NestApplicationOptionRequest
    {
        public NestApplicationOptionRequest(
            string name,
            string value,
            IEnumerable<(string Type, string Location)> typesToImport)
        {
            Name = name;
            Value = value;
            TypesToImport = typesToImport;
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public IEnumerable<(string Type, string Location)> TypesToImport { get; set; }
    }
}
