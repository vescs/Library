using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Tickets;
using Library.Infrastructure.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService, ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
        {
            _ticketService = ticketService;
        }

        [HttpGet("{ticketId}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid eventId, Guid ticketId)
        {
            var ticket = await _ticketService.GetAsync(UserId, eventId, ticketId);
            if (ticket == null)
            {
                return NotFound();
            }

            return Json(ticket);
        }

        [HttpPost("purchase")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] PurchaseTickets command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [HttpDelete("cancel")]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody]CancelTickets command)
        {
            await DispatchAsync(command);
            return NoContent();
        }
    }
}
