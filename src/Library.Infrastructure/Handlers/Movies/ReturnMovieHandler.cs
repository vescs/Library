using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Movies;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Movies
{
    public class ReturnMovieHandler : ICommandHandler<ReturnMovie>
    {
        private readonly IMovieService _movieService;

        public ReturnMovieHandler(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task HandleAsync(ReturnMovie command)
        {
            await _movieService.ReturnAsync(command.Id, command.UserId);
        }
    }
}
