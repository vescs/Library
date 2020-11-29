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
        public CreateNewspaperHandler(INewspaperService newspaperService)
        {
            _newspaperService = newspaperService;
        }
        public async Task HandleAsync(CreateNewspaper command)
        {
            command.Id = Guid.NewGuid();
            await _newspaperService.CreateAsync(command.Id, command.Title, command.Description, 
                command.Type, command.Quantity, command.ReleaseDate);

        }
    }
}
