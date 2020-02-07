using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Order.Contract.Orders;
using Shop.Order.Contract.Orders.Dto;
using Shop.Web.Utils.WaitingTasksStore;

namespace Shop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderServiceContract _orderServiceContract;
        private readonly IWaitingTasksStore _waitingTasksStore;

        public OrdersController(IOrderServiceContract orderServiceContract, IWaitingTasksStore waitingTasksStore)
        {
            _orderServiceContract = orderServiceContract;
            _waitingTasksStore = waitingTasksStore;
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
            var correlationId = Guid.NewGuid().ToString();

            var resTask = _waitingTasksStore.Add<int>(correlationId);
            
            //await ir not await? ))
            _orderServiceContract.CreateOrderAsync(correlationId, createOrderDto);

            var orderId = await resTask;

            return orderId;
        }
    }
}
