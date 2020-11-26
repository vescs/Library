using Library.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Route("events/{eventId}/tickets")]
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [HttpGet("{ticketId}")]
        public async Task<IActionResult> Get(Guid userId, Guid eventId, Guid ticketId)
        {
            var ticket = await _ticketService.GetAsync(userId, eventId, ticketId);
            if (ticket == null)
            {
                return NotFound();
            }

            return Json(ticket);
        }
        [HttpPost("purchase/{amount}")]
        public async Task<IActionResult> Post(Guid userId, Guid eventId, int amount, bool seat)
        {
            await _ticketService.PurchaseAsync(userId, eventId, amount, seat);
            return NoContent();
        }
        [HttpDelete("cancel/{amount}")]
        public async Task<IActionResult> Delete(Guid userId, Guid eventId, int amount, bool seat)
        {
            await _ticketService.CancelAsync(userId, eventId, amount, seat);
            return NoContent();
        }
    }
}
