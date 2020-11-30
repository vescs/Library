using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Books;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Books
{
    public class AddBookHandler : ICommandHandler<AddBook>
    {
        private readonly IBookService _bookService;
        public AddBookHandler(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task HandleAsync(AddBook command)
        {
            await _bookService.IncreaseQuantityAsync(command.Id, command.Quantity);
        }
    }
}
