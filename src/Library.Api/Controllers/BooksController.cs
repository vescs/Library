using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Books;
using Library.Infrastructure.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        
        public BooksController(IBookService bookService, ICommandDispatcher commandDispatcher) 
            : base(commandDispatcher)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string title)
        {
            var books = await _bookService.BrowseAsync(title);
            return Json(books);
        }
        [HttpGet("author/{author}")]
        public async Task<IActionResult> GetAuthors(string author)
        {
            var books = await _bookService.BrowseAuthorsAsync(author);
            return Json(books);
        }
        [HttpGet("publishing_house/{house}")]
        public async Task<IActionResult> GetHouses(string house)
        {
            var books = await _bookService.BrowseHousesAsync(house);
            return Json(books);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var book = await _bookService.GetAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Json(book);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
        [HttpPost]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Post([FromBody]CreateBook command)
        {
            await DispatchAsync(command);
            return Created($"/books/{command.Id}", null);
        }
        [HttpPut("update")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put([FromBody]UpdateBook command)
        {
            await DispatchAsync(command);
            return NoContent();
        }
        [HttpPut("lend")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody]LendBook command)
        {
            await DispatchAsync(command);
            return NoContent();
        }
        [HttpPut("return")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody]ReturnBook command)
        {
            await DispatchAsync(command);
            return NoContent();
        }
        [HttpPut("add")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put([FromBody]AddBook command)
        {
            await DispatchAsync(command);
            return NoContent();
        }
        [HttpPut("remove")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put([FromBody]RemoveBook command)
        {
            await DispatchAsync(command);
            return NoContent();
        }
    }
}
