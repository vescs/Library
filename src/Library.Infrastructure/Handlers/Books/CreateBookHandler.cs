using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Books;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Books
{
    public class CreateBookHandler : ICommandHandler<CreateBook>
    {
        private readonly IBookService _bookService;
        private readonly IFluentHandler _fluentHandler;

        public CreateBookHandler(IBookService bookService, IFluentHandler fluentHandler)
        {
            _bookService = bookService;
            _fluentHandler = fluentHandler;
        }

        public async Task HandleAsync(CreateBook command)
            => await _fluentHandler
                .Run(async () =>
                {
                    command.Id = Guid.NewGuid();
                    await _bookService.CreateAsync(command.Id, command.Title, command.Description, command.Author,
                        command.Pages, command.PublishingHouse, command.Quantity, command.PremiereDate);
                })
                .ExecuteAsync();
    }
}
