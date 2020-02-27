using Microsoft.AspNetCore.Mvc;
using IMvcUrlHelper = Microsoft.AspNetCore.Mvc.IUrlHelper;
using IUrlHelper = Shop.Framework.Interfaces.Services.IUrlHelper;

namespace Shop.Framework.Implementation.Services
{
    public class McvUrlHelper : IUrlHelper
    {
        private readonly IMvcUrlHelper _urlHelper;

        public McvUrlHelper(IMvcUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public string GetOrderDetails(int orderId)
        {
            return _urlHelper.Action("", "order", new object[] {orderId}, "http");
        }
    }
}
