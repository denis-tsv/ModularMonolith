namespace Shop.Framework.Interfaces.Services
{
    public interface IRequestContext
    {
        void AddValue(string key, object value);

        T GetValue<T>(string key);
    }
}
