using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Tickets;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Tickets
{
    public class PurchaseTicketsHandler : ICommandHandler<PurchaseTickets>
    {
        private readonly ITicketService _ticketService;

        public PurchaseTicketsHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task HandleAsync(PurchaseTickets command)
        {
            await _ticketService.PurchaseAsync(command.UserId, command.EventId, command.Amount, command.Seat);
        }
    }
}
