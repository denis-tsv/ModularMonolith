using System.Threading.Tasks;
using Shop.Order.Contract.Dto;

namespace Shop.Order.Contract
{
    public interface IOrderContract
    {
        Task<OrderDto> GetOrderAsync(int id);
        Task CreateOrderAsync(CreateOrderDto createOrderDto);
        Task CancelCreateOrderAsync(int orderId);
    }
}
