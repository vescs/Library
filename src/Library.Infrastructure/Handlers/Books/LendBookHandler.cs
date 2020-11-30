using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Books;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Books
{
    public class LendBookHandler : ICommandHandler<LendBook>
    {
        private readonly IBookService _bookService;
        public LendBookHandler(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task HandleAsync(LendBook command)
        {
            await _bookService.LendAsync(command.BookId, command.UserId);
        }
    }
}
