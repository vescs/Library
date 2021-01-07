using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Movies;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Movies
{
    public class AddMovieHandler : ICommandHandler<AddMovie>
    {
        private readonly IMovieService _movieService;
        private readonly IFluentHandler _fluentHandler;

        public AddMovieHandler(IMovieService movieService, IFluentHandler fluentHandler)
        {
            _movieService = movieService;
            _fluentHandler = fluentHandler;
        }

        public async Task HandleAsync(AddMovie command)
            => await _fluentHandler
                .Run(async () => await _movieService.IncreaseQuantityAsync(command.Id, command.Quantity))
                .ExecuteAsync();
    }
}
