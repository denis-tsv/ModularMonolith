using System.Threading.Tasks;
using Shop.Identity.Contract.Identity.Dto;

namespace Shop.Identity.Contract.Identity
{
    public interface IIdentityService
    {
        Task LoginAsync(LoginDto loginDto);
    }
}
