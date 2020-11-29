using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Books;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Books
{
    public class UpdateBookHandler : ICommandHandler<UpdateBook>
    {
        private readonly IBookService _bookService;
        public UpdateBookHandler(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task HandleAsync(UpdateBook command)
        {
            await _bookService.UpdateAsync(command.Id, command.Title, command.Description);
        }
    }
}
