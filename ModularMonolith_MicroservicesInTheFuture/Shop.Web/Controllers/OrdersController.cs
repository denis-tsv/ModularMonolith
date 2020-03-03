using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Framework.Interfaces.Cancel;
using Shop.Order.Contract;
using Shop.Order.Contract.Dto;

namespace Shop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderContract _order;
        private readonly ICancelService _cancelService;

        public OrdersController(IOrderContract order, ICancelService cancelService)
        {
            _order = order;
            _cancelService = cancelService;
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
            await OperationWithCancel(_order.CreateOrderAsync(createOrderDto));
        }

        private async Task<TResult> OperationWithCancel<TResult>(Task<TResult> operation)
        {
            try
            {
                return await operation;
            }
            catch
            {
                await _cancelService.CancelAllAsync();

                throw; //for generation of 500 http code via ExceptionHandlerMiddleware
            }
        }

        private async Task OperationWithCancel(Task operation)
        {
            try
            {
                await operation;
            }
            catch
            {
                await _cancelService.CancelAllAsync();

                throw; //for generation of 500 http code via ExceptionHandlerMiddleware
            }
        }
    }
}
