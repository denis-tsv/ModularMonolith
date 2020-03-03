using System.Collections.Generic;
using Shop.Framework.Interfaces.Services;

namespace Shop.Framework.Implementation.Services
{
    public class RequestContext : IRequestContext
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
