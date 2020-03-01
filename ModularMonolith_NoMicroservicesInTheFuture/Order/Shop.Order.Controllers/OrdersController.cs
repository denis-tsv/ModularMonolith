using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Order.Contract;
using Shop.Order.Contract.Dto;

namespace Shop.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderContract _orderContract;

        public OrdersController(IOrderContract orderContract)
        {
            _orderContract = orderContract;
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            return await _orderContract.GetOrderAsync(id);
        }

        // POST api/orders
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateOrderDto createOrderDto)
        {
            return await _orderContract.CreateOrderAsync(createOrderDto);
        }
    }
}
