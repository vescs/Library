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

        public CreateEventHandler(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task HandleAsync(CreateEvent command)
        {
            command.Id = Guid.NewGuid();
            await _eventService.CreateAsync(command.Id, command.Name, command.Description,
                command.StartDate, command.EndDate);
        }
    }
}
