using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Events;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Events
{
    public class UpdateEventHandler : ICommandHandler<UpdateEvent>
    {
        private readonly IEventService _eventService;

        public UpdateEventHandler(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task HandleAsync(UpdateEvent command)
        {
            await _eventService.UpdateAsync(command.Id, command.Description);
        }
    }
}
