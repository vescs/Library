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
        private readonly IFluentHandler _fluentHandler;

        public UpdateEventHandler(IEventService eventService, IFluentHandler fluentHandler)
        {
            _eventService = eventService;
            _fluentHandler = fluentHandler;
        }

        public async Task HandleAsync(UpdateEvent command)
            => await _fluentHandler
                .Run(async () => await _eventService.UpdateAsync(command.Id, command.Description))
                .ExecuteAsync();
    }
}
