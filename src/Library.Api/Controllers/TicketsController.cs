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
        public TicketsController(ITicketService ticketService)
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
        [HttpPost("purchase/{amount}")]
        [Authorize]
        public async Task<IActionResult> Post(Guid eventId, int amount, bool seat)
        {
            await _ticketService.PurchaseAsync(UserId, eventId, amount, seat);
            return NoContent();
        }
        [HttpDelete("cancel/{amount}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid eventId, int amount, bool seat)
        {
            await _ticketService.CancelAsync(UserId, eventId, amount, seat);
            return NoContent();
        }
    }
}
