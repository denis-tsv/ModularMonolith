using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Shop.Web.Utils
{
    public class InternalControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            return typeof(ControllerBase).IsAssignableFrom(typeInfo);
        }
    }
}
