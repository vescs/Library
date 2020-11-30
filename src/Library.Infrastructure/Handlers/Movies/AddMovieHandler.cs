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

        public AddMovieHandler(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task HandleAsync(AddMovie command)
        {
            await _movieService.IncreaseQuantityAsync(command.Id, command.Quantity);
        }
    }
}
