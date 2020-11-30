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
        public ReturnBookHandler(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task HandleAsync(ReturnBook command)
        {
            await _bookService.ReturnAsync(command.Id, command.UserId);
        }
    }
}
