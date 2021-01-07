using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Newspapers;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Newspapers
{
    public class CreateNewspaperHandler : ICommandHandler<CreateNewspaper>
    {
        private readonly INewspaperService _newspaperService;
        private readonly IFluentHandler _fluentHandler;

        public CreateNewspaperHandler(INewspaperService newspaperService, IFluentHandler fluentHandler)
        {
            _newspaperService = newspaperService;
            _fluentHandler = fluentHandler;
        }


        public async Task HandleAsync(CreateNewspaper command)
            => await _fluentHandler
                .Run(async () =>
                {
                    command.Id = Guid.NewGuid();
                    await _newspaperService.CreateAsync(command.Id, command.Title, command.Description,
                            command.Type, command.Quantity, command.ReleaseDate);
                })
                .ExecuteAsync();
                
    }
}
