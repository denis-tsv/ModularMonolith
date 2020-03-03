using System.Threading.Tasks;
using Shop.Identity.Contract.Dto;

namespace Shop.Identity.Contract
{
    public interface IIdentityContract
    {
        Task LoginAsync(LoginDto loginDto);
    }
}
