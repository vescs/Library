using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Newspapers;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Newspapers
{
    public class LendNewspaperHandler : ICommandHandler<LendNewspaper>
    {
        private readonly INewspaperService _newspaperService;

        public LendNewspaperHandler(INewspaperService newspaperService)
        {
            _newspaperService = newspaperService;
        }

        public async Task HandleAsync(LendNewspaper command)
        {
            await _newspaperService.LendAsync(command.Id, command.UserId);
        }
    }
}
