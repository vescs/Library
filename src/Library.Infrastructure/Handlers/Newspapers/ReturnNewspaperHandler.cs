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
        private readonly IFluentHandler _fluentHandler;

        public ReturnNewspaperHandler(INewspaperService newspaperService, IFluentHandler fluentHandler)
        {
            _newspaperService = newspaperService;
            _fluentHandler = fluentHandler;
        }

        public async Task HandleAsync(ReturnNewspaper command)
            => await _fluentHandler
                .Run(async () => await _newspaperService.ReturnAsync(command.Id, command.UserId))
                .ExecuteAsync();
    }
}
