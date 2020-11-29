using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Newspapers;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Newspapers
{
    public class UpdateNewspaperHandler : ICommandHandler<UpdateNewspaper>
    {
        private readonly INewspaperService _newspaperService;
        public UpdateNewspaperHandler(INewspaperService newspaperService)
        {
            _newspaperService = newspaperService;
        }
        public async Task HandleAsync(UpdateNewspaper command)
        {
            await _newspaperService.UpdateAsync(command.Id, command.Description);
        }
    }
}
