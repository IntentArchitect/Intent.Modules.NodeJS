namespace Intent.Modules.NestJS.Core.Events
{
    public class EnvironmentVariableRequest
    {
        public EnvironmentVariableRequest(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }
    }
}
