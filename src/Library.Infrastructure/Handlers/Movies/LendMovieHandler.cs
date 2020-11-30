using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Movies;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Movies
{
    public class LendMovieHandler : ICommandHandler<LendMovie>
    {
        private readonly IMovieService _movieService;

        public LendMovieHandler(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task HandleAsync(LendMovie command)
        {
            await _movieService.LendAsync(command.Id, command.UserId);
        }
    }
}
