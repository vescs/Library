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
        private readonly IFluentHandler _fluentHandler;

        public PurchaseTicketsHandler(ITicketService ticketService, IFluentHandler fluentHandler)
        {
            _ticketService = ticketService;
            _fluentHandler = fluentHandler;
        }

        public async Task HandleAsync(PurchaseTickets command)
            => await _fluentHandler
                .Run(async () => await _ticketService.PurchaseAsync(command.UserId, command.EventId, command.Amount, command.Seat))
                .ExecuteAsync();
    }
}
