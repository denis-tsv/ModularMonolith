using System.Threading.Tasks;
using Shop.Identity.UseCases.Identity.Dto;

namespace Shop.Identity.Contract
{
    public interface IIdentityContract
    {
        Task LoginAsync(LoginDto dto);
    }
}
