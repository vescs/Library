using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Newspapers;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Newspapers
{
    public class RemoveNewspaperHandler : ICommandHandler<RemoveNewspaper>
    {
        private readonly INewspaperService _newspaperService;

        public RemoveNewspaperHandler(INewspaperService newspaperService)
        {
            _newspaperService = newspaperService;
        }

        public async Task HandleAsync(RemoveNewspaper command)
        {
            await _newspaperService.DecreaseQuantityAsync(command.Id, command.Quantity);
        }
    }
}
