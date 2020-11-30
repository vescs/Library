using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Books;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Books
{
    public class RemoveBookHandler : ICommandHandler<RemoveBook>
    {
        private readonly IBookService _bookService;
        public RemoveBookHandler(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task HandleAsync(RemoveBook command)
        {
            await _bookService.DecreaseQuantityAsync(command.Id, command.Quantity);
        }
    }
}
