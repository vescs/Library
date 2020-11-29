using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Movies;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Movies
{
    public class UpdateMovieHandler : ICommandHandler<UpdateMovie>
    {
        private readonly IMovieService _movieService;
        public UpdateMovieHandler(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public async Task HandleAsync(UpdateMovie command)
        {
            await _movieService.UpdateAsync(command.Id, command.Description);

        }
    }
}
