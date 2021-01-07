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
        private readonly IFluentHandler _fluentHandler;

        public AddTicketsHandler(IEventService eventService, IFluentHandler fluentHandler)
        {
            _eventService = eventService;
            _fluentHandler = fluentHandler;
        }

        public async Task HandleAsync(AddTickets command)
            => await _fluentHandler
                .Run(async () => await _eventService.AddTicketsAsync(command.EventId, command.Amount, command.Price, command.Seat))
                .ExecuteAsync();
    }
}
