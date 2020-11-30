using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Newspapers;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Newspapers
{
    public class ReturnNewspaperHandler : ICommandHandler<ReturnNewspaper>
    {
        private readonly INewspaperService _newspaperService;

        public ReturnNewspaperHandler(INewspaperService newspaperService)
        {
            _newspaperService = newspaperService;
        }

        public async Task HandleAsync(ReturnNewspaper command)
        {
            await _newspaperService.ReturnAsync(command.Id, command.UserId);
        }
    }
}
