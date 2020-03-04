using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Order.Contract;
using Shop.Order.Contract.Dto;

namespace Shop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderContract _order;

        public OrdersController(IOrderContract order)
        {
            _order = order;
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            return await _order.GetOrderAsync(id);
            
        }

        // POST api/orders
        [HttpPost]
        public async Task Post([FromBody] CreateOrderDto createOrderDto)
        {
            await _order.CreateOrderAsync(createOrderDto);
        }
    }
}
