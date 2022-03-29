using Microsoft.AspNetCore.Mvc;
using IMvcUrlHelper = Microsoft.AspNetCore.Mvc.IUrlHelper;

namespace Shop.Framework.UseCases.Implementation.Services
{
    internal class McvUrlHelper : Interfaces.Services.IUrlHelper
    {
        private readonly IMvcUrlHelper _urlHelper;

        public McvUrlHelper(IMvcUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public string GetOrderDetails(int orderId)
        {
            return _urlHelper.Action("", "orders", new object[] {orderId}, "http");
        }
    }
}
