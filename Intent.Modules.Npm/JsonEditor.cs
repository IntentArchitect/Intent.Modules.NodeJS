using Newtonsoft.Json.Linq;

namespace Intent.Modules.Npm
{
    public class JsonEditor
    {
        private readonly dynamic _jsonObject;

        public JsonEditor(dynamic jsonObject)
        {
            _jsonObject = jsonObject;
        }

        public dynamic Value => _jsonObject;

        public bool PropertyExists(string key)
        {
            return _jsonObject[key] != null;
        }

        public dynamic GetProperty(string key)
        {
            return PropertyExists(key) ? _jsonObject[key] : default;
        }

        public T GetPropertyAs<T>(string key)
        {
            if (!PropertyExists(key))
            {
                return default;
            }

            if (_jsonObject[key] is JObject)
            {
                return ((JObject)_jsonObject[key]).ToObject<T>();
            }
            return (T)_jsonObject[key];
        }

        public void AddPropertyIfNotExists(string key, object value)
        {
            if (PropertyExists(key))
            {
                return;
            }

            if (value is bool b)
            {
                _jsonObject[key] = b;
            }
            else if (value is int i)
            {
                _jsonObject[key] = i;
            }
            else if (value is string s)
            {
                _jsonObject[key] = s;
            }
            else
            {
                _jsonObject[key] = JObject.FromObject(value);
            }
        }

        public void SetProperty(string key, string value)
        {
            _jsonObject[key] = value;
        }

        public void SetProperty(string key, object value)
        {
            _jsonObject[key] = JObject.FromObject(value);
        }
    }
}