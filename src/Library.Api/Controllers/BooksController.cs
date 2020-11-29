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
            await CommandDispatcher.DispatchAsync(command);
            return Created($"/books/{command.Id}", null);
        }
        [HttpPut("{command.Id}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put([FromBody]UpdateBook command)
        {
            await CommandDispatcher.DispatchAsync(command);
            return NoContent();
        }
        [HttpPut("lend/{id}")]
        [Authorize]
        public async Task<IActionResult> PutLend(Guid id)
        {
            await _bookService.LendAsync(id, UserId);
            return NoContent();
        }
        [HttpPut("return/{id}")]
        [Authorize]
        public async Task<IActionResult> PutReturn(Guid id)
        {
            await _bookService.ReturnAsync(id, UserId);
            return NoContent();
        }
        [HttpPut("add/{id}/{quantity}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> PutAdd(Guid id, int quantity)
        {
            await _bookService.IncreaseQuantityAsync(id, quantity);
            return NoContent();
        }
        [HttpPut("remove/{id}/{quantity}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> PutRemove(Guid id, int quantity)
        {
            await _bookService.DecreaseQuantityAsync(id, quantity);
            return NoContent();
        }
    }
}
