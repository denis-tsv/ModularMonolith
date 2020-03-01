using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Identity.Contract;
using Shop.Identity.UseCases.Identity.Dto;

namespace Shop.Identity.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityContract _identityContract;

        public IdentityController(IIdentityContract identityContract)
        {
            _identityContract = identityContract;
        }

        [HttpPost]
        public async Task Login([FromBody]LoginDto loginDto)
        {
            await _identityContract.LoginAsync(loginDto);
        }
    }
}
