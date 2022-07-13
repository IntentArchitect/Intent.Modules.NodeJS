using Newtonsoft.Json.Linq;

namespace Intent.Modules.Npm
{
    public class JsonEditor
    {
        public JsonEditor(JToken jsonObject)
        {
            Value = jsonObject;
        }

        public JToken Value { get; }

        public bool PropertyExists(string key)
        {
            return Value[key] != null;
        }

        public JToken GetProperty(string key)
        {
            return PropertyExists(key) ? Value[key] : default;
        }

        public T GetPropertyAs<T>(string key)
        {
            if (!PropertyExists(key))
            {
                return default;
            }

            if (Value[key] is JObject)
            {
                return ((JObject)Value[key]).ToObject<T>();
            }

            return Value[key]!.Value<T>();
        }

        public void AddPropertyIfNotExists(string key, object value)
        {
            if (PropertyExists(key))
            {
                return;
            }

            if (value is bool b)
            {
                Value[key] = b;
            }
            else if (value is int i)
            {
                Value[key] = i;
            }
            else if (value is string s)
            {
                Value[key] = s;
            }
            else
            {
                Value[key] = JObject.FromObject(value);
            }
        }

        public void SetProperty(string key, string value)
        {
            Value[key] = value;
        }

        public void SetProperty(string key, object value)
        {
            Value[key] = JObject.FromObject(value);
        }
    }
}