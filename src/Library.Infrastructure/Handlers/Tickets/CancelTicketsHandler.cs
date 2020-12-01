using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Tickets;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Tickets
{
    public class CancelTicketsHandler : ICommandHandler<CancelTickets>
    {
        private readonly ITicketService _ticketService;

        public CancelTicketsHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task HandleAsync(CancelTickets command)
        {
            await _ticketService.CancelAsync(command.UserId, command.EventId, command.Amount, command.Seat);
        }
    }
}
