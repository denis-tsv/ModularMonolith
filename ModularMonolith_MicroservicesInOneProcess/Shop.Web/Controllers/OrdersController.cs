using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Order.Contract.Orders;
using Shop.Order.Contract.Orders.Dto;

namespace Shop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderServiceContract _orderServiceContract;

        public OrdersController(IOrderServiceContract orderServiceContract)
        {
            _orderServiceContract = orderServiceContract;
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            return await _orderServiceContract.GetOrderAsync(id);
            
        }

        // POST api/orders
        [HttpPost]
        public async Task<int> Post([FromBody] CreateOrderDto createOrderDto)
        {
            var orderId = await _orderServiceContract.CreateOrderAsync(createOrderDto);
            return orderId;
        }
    }
}
