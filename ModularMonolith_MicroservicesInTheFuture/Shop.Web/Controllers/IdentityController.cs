using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Identity.Contract.Identity;
using Shop.Identity.Contract.Identity.Dto;

namespace Shop.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task Login([FromBody]LoginDto loginDto)
        {
            await _identityService.LoginAsync(loginDto);
        }

        //Register

        //Logout

        //Forgot password

        //Confirm email
    }
}
