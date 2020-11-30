using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Movies;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Movies
{
    public class RemoveMovieHandler : ICommandHandler<RemoveMovie>
    {
        private readonly IMovieService _movieService;

        public RemoveMovieHandler(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task HandleAsync(RemoveMovie command)
        {
            await _movieService.DecreaseQuantityAsync(command.Id, command.Quantity);
        }
    }
}
