using Library.Infrastructure.Commands.Newspapers;
using Library.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Route("[controller]")]
    public class NewspapersController : Controller
    {
        private readonly INewspaperService _newspaperService;
        public NewspapersController(INewspaperService newspaperService)
        {
            _newspaperService = newspaperService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string title = "")
        {
            var newspapers = await _newspaperService.BrowseAsync(title);
            return Json(newspapers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var newspaper = await _newspaperService.GetAsync(id);
            if(newspaper == null)
            {
                return NotFound();
            }
            return Json(newspaper);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateNewspaper command)
        {
            command.Id = Guid.NewGuid();
            await _newspaperService.CreateAsync(command.Id, command.Title, command.Description, command.Type, command.Quantity, command.ReleaseDate);
            return Created($"newspapers/{command.Id}", null);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]UpdateNewspaper command, Guid id)
        {
            await _newspaperService.UpdateAsync(id, command.Description);
            return NoContent();
        }
        [HttpPut("lend/{id}")]
        public async Task<IActionResult> Put([FromBody]LendNewspaper command, Guid id)
        {
            await _newspaperService.LendAsync(id, command.userId);
            return NoContent();
        }
        [HttpPut("return/{id}")]
        public async Task<IActionResult> Put([FromBody]ReturnNewspaper command, Guid id)
        {
            await _newspaperService.ReturnAsync(id, command.userId);
            return NoContent();
        }
        [HttpPut("add/{id}/{quantity}")]
        public async Task<IActionResult> PutAdd(Guid id, int quantity)
        {
            await _newspaperService.IncreaseQuantityAsync(id, quantity);
            return NoContent();
        }
        [HttpPut("remove/{id}/{quantity}")]
        public async Task<IActionResult> PutRemove(Guid id, int quantity)
        {
            await _newspaperService.DecreaseQuantityAsync(id, quantity);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _newspaperService.DeleteAsync(id);
            return NoContent();
        }
    }
}
