using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Events;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Events
{
    public class CreateEventHandler : ICommandHandler<CreateEvent>
    {
        private readonly IEventService _eventService;
        private readonly IFluentHandler _fluentHandler;

        public CreateEventHandler(IEventService eventService, IFluentHandler fluentHandler)
        {
            _eventService = eventService;
            _fluentHandler = fluentHandler;
        }

        public async Task HandleAsync(CreateEvent command)
            => await _fluentHandler
                .Run(async () =>
                {
                    command.Id = Guid.NewGuid();
                    await _eventService.CreateAsync(command.Id, command.Name, command.Description,
                        command.StartDate, command.EndDate);
                })
                .ExecuteAsync();
    }
}
