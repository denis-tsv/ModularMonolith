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
        private readonly IOrderServiceContract _orderService;

        public OrdersController(IOrderServiceContract orderServcie)
        {
            _orderService = orderServcie;
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            return await _orderService.GetOrderAsync(id);
            
        }

        // POST api/orders
        [HttpPost]
        public async Task Post([FromBody] CreateOrderDto createOrderDto)
        {
            await _orderService.CreateOrderAsync(createOrderDto);
        }
    }
}
