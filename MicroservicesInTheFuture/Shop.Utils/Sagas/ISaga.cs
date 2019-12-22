using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Utils.Sagas
{
    public interface ISaga
    {
        void AddValue(string key, object value);

        T GetValue<T>(string key);
    }
}
