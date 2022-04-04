using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Order.UseCases.Orders.Dto;
using Shop.Web.Sagas;

namespace Shop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    internal class OrdersController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateOrderDto createOrderDto, CancellationToken cancellationToken, [FromServices]CreateOrderSaga saga)
        {
            await saga.Start(createOrderDto, cancellationToken);
            var orderId = saga.GetResult();
            if (orderId == null) throw new Exception("Unable to create order");
            return orderId;
        }
    }
}
