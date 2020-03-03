using Microsoft.AspNetCore.Mvc;
using IMvcUrlHelper = Microsoft.AspNetCore.Mvc.IUrlHelper;
using IUrlHelper = Shop.Framework.Interfaces.Services.IUrlHelper;

namespace Shop.Framework.Implementation.Services
{
    internal class MvcUrlHelper : IUrlHelper
    {
        private readonly IMvcUrlHelper _mvcUrlHelper;

        public MvcUrlHelper(IMvcUrlHelper mvcUrlHelper)
        {
            _mvcUrlHelper = mvcUrlHelper;
        }

        public string Action(string action, string controller, object values, string protocol)
        {
            return _mvcUrlHelper.Action(action, controller, values, protocol);
        }
    }
}
