using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Books;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Books
{
    public class ReturnBookHandler : ICommandHandler<ReturnBook>
    {
        private readonly IBookService _bookService;
        private readonly IFluentHandler _fluentHandler;

        public ReturnBookHandler(IBookService bookService, IFluentHandler fluentHandler)
        {
            _bookService = bookService;
            _fluentHandler = fluentHandler;
        }

        public async Task HandleAsync(ReturnBook command)
            => await _fluentHandler
                .Run(async () => await _bookService.ReturnAsync(command.Id, command.UserId))
                .ExecuteAsync();
    }
}
