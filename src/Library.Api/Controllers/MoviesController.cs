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
        
        [HttpPost]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Post([FromBody]CreateMovie command)
        {
            await CommandDispatcher.DispatchAsync(command);
            return Created($"/movies/{command.Id}", null);
        }
        [HttpPut]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put([FromBody]UpdateMovie command)
        {
            var movie = await _movieService.GetAsync(command.Id);
            if (movie == null)
            {
                return NotFound();
            }
            await CommandDispatcher.DispatchAsync(command);
            return NoContent();
        }
        [HttpPut("lend/{id}")]
        [Authorize]
        public async Task<IActionResult> PutLend(Guid id)
        {
            await _movieService.LendAsync(id, UserId);
            return NoContent();
        }
        [HttpPut("return/{id}")]
        [Authorize]
        public async Task<IActionResult> PutReturn(Guid id)
        {
            await _movieService.ReturnAsync(id, UserId);
            return NoContent();
        }
        [HttpPut("add/{id}/{quantity}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> PutAdd(Guid id, int quantity)
        {
            await _movieService.IncreaseQuantityAsync(id, quantity);
            return NoContent();
        }
        [HttpPut("remove/{id}/{quantity}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> PutRemove(Guid id, int quantity)
        {
            await _movieService.DecreaseQuantityAsync(id, quantity);
            return NoContent();
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
    }
}
