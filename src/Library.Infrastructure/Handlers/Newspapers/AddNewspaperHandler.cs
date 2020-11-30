using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Newspapers;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Newspapers
{
    public class AddNewspaperHandler : ICommandHandler<AddNewspaper>
    {
        private readonly INewspaperService _newspaperService;

        public AddNewspaperHandler(INewspaperService newspaperService)
        {
            _newspaperService = newspaperService;
        }

        public async Task HandleAsync(AddNewspaper command)
        {
            await _newspaperService.IncreaseQuantityAsync(command.Id, command.Quantity);
        }
    }
}
