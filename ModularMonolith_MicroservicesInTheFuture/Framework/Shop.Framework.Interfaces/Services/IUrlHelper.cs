namespace Shop.Framework.Interfaces.Services
{
    public interface IUrlHelper
    {
        string Action(string action, string controller, object values, string protocol);
    }
}
