using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Events;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Events
{
    public class AddTicketsHandler : ICommandHandler<AddTickets>
    {
        private readonly IEventService _eventService;

        public AddTicketsHandler(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task HandleAsync(AddTickets command)
        {
            await _eventService.AddTicketsAsync(command.EventId, command.Amount, command.Price, command.Seat);
        }
    }
}
