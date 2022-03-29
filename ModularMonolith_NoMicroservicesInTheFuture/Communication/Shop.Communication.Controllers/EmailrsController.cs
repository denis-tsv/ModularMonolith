using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Communication.UseCases.Emails.Dto;
using Shop.Communication.UseCases.Emails.Queries.GetEmails;

namespace Shop.Communication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    internal class EmailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/emails
        [HttpGet]
        public async Task<ActionResult<EmailDto[]>> Get()
        {
            return await _mediator.Send(new GetEmailsRequest());
        }
    }
}
