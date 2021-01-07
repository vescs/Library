using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Movies;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Movies
{
    public class CreateMovieHandler : ICommandHandler<CreateMovie>
    {
        private readonly IMovieService _movieService;
        private readonly IFluentHandler _fluentHandler;

        public CreateMovieHandler(IMovieService movieService, IFluentHandler fluentHandler)
        {
            _movieService = movieService;
            _fluentHandler = fluentHandler;
        }

        public async Task HandleAsync(CreateMovie command)
            => await _fluentHandler
                .Run(async () =>
                {
                    command.Id = Guid.NewGuid();
                    await _movieService.CreateAsync(command.Id, command.Title, command.Description, command.Director,
                        command.Length, command.Quantity, command.PremiereDate);
                })
                .ExecuteAsync();
    }
}
