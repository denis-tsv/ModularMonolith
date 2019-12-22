using System.Collections.Generic;

namespace Shop.Utils.Sagas
{
    public class Saga : ISaga
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        public void AddValue(string key, object value)
        {
            _values.Add(key, value);
        }

        public T GetValue<T>(string key)
        {
            return (T) _values[key];
        }
    }
}
