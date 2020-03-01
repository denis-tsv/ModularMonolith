using System.Threading.Tasks;
using Shop.Order.Contract.Dto;

namespace Shop.Order.Contract
{
    public interface IOrderContract
    {
        Task<int> CreateOrderAsync(CreateOrderDto createOrderDto);

        Task<OrderDto> GetOrderAsync(int orderId);
    }
}
