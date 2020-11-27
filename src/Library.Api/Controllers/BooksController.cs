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
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
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
        [Authorize]
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
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateBook command)
        {
            command.Id = Guid.NewGuid();
            await _bookService.CreateAsync(command.Id, command.Title, command.Description, command.Author, 
                command.Pages, command.PublishingHouse, command.Quantity, command.PremiereDate);
            return Created($"/books/{command.Id}", null);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]UpdateBook command, Guid id)
        {
            await _bookService.UpdateAsync(id, command.Title, command.Description);
            return NoContent();
        }
        [HttpPut("lend/{id}")]
        public async Task<IActionResult> Put([FromBody]LendBook command, Guid id)
        {
            await _bookService.LendAsync(id, command.UserId);
            return NoContent();
        }
        [HttpPut("return/{id}")]
        public async Task<IActionResult> Put([FromBody]ReturnBook command, Guid id)
        {
            await _bookService.ReturnAsync(id, command.UserId);
            return NoContent();
        }
        [HttpPut("add/{id}/{quantity}")]
        public async Task<IActionResult> PutAdd(Guid id, int quantity)
        {
            await _bookService.IncreaseQuantityAsync(id, quantity);
            return NoContent();
        }
        [HttpPut("remove/{id}/{quantity}")]
        public async Task<IActionResult> PutRemove(Guid id, int quantity)
        {
            await _bookService.DecreaseQuantityAsync(id, quantity);
            return NoContent();
        }
    }
}
