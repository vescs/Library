using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Movies;
using Library.Infrastructure.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService, ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string title = "")
        {
            var movies = await _movieService.BrowseAsync(title);
            return Json(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var movie = await _movieService.GetAsync(id);
            if(movie == null)
            {
                return NotFound();
            }
            return Json(movie);
        }

        [HttpDelete]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var movie = await _movieService.GetAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            await _movieService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Post([FromBody]CreateMovie command)
        {
            await DispatchAsync(command);
            return Created($"/movies/{command.Id}", null);
        }

        [HttpPut("update")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put([FromBody]UpdateMovie command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [HttpPut("lend")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody]LendMovie command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [HttpPut("return")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody]ReturnMovie command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [HttpPut("add")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put([FromBody]AddMovie command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [HttpPut("remove")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put([FromBody]RemoveMovie command)
        {
            await DispatchAsync(command);
            return NoContent();
        }
        
    }
}
