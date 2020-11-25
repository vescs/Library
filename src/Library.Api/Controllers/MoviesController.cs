using Library.Infrastructure.Commands.Movies;
using Library.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Route("[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
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
        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetTitle(string title)
        {
            var movie = await _movieService.GetAsync(title);
            if (movie == null)
            {
                return NotFound();
            }
            return Json(movie);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateMovie command)
        {
            command.Id = Guid.NewGuid();
            await _movieService.CreateAsync(command.Id, command.Title, command.Description, command.Director, 
                command.Length, command.Quantity, command.PremiereDate);
            return Created($"/movies/{command.Id}", null);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateMovie command, Guid id)
        {
            var movie = await _movieService.GetAsync(id);
            if(movie == null)
            {
                return NotFound();
            }
            await _movieService.UpdateAsync(id, command.Description);
            return NoContent();
        }
        [HttpPut("lend/{id}")]
        public async Task<IActionResult> Put([FromBody]LendMovie command, Guid id)
        {
            await _movieService.LendAsync(id, command.UserId);
            return NoContent();
        }
        [HttpPut("return/{id}")]
        public async Task<IActionResult> Put([FromBody]ReturnMovie command, Guid id)
        {
            await _movieService.ReturnAsync(id, command.UserId);
            return NoContent();
        }
        [HttpPut("add/{id}/{quantity}")]
        public async Task<IActionResult> PutAdd(Guid id, int quantity)
        {
            await _movieService.IncreaseQuantityAsync(id, quantity);
            return NoContent();
        }
        [HttpPut("remove/{id}/{quantity}")]
        public async Task<IActionResult> PutRemove(Guid id, int quantity)
        {
            await _movieService.DecreaseQuantityAsync(id, quantity);
            return NoContent();
        }
        [HttpDelete]
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
