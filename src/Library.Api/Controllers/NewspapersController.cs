using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Newspapers;
using Library.Infrastructure.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    public class NewspapersController : ControllerBase
    {
        private readonly INewspaperService _newspaperService;
        public NewspapersController(INewspaperService newspaperService, ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
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
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Post([FromBody]CreateNewspaper command)
        {
            await CommandDispatcher.DispatchAsync(command);
            return Created($"newspapers/{command.Id}", null);
        }
        [HttpPut("{command.Id}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put([FromBody]UpdateNewspaper command)
        {
            await CommandDispatcher.DispatchAsync(command);
            return NoContent();
        }
        [HttpPut("lend/{id}")]
        [Authorize]
        public async Task<IActionResult> PutLend(Guid id)
        {
            await _newspaperService.LendAsync(id, UserId);
            return NoContent();
        }
        [HttpPut("return/{id}")]
        [Authorize]
        public async Task<IActionResult> PutReturn(Guid id)
        {
            await _newspaperService.ReturnAsync(id, UserId);
            return NoContent();
        }
        [HttpPut("add/{id}/{quantity}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> PutAdd(Guid id, int quantity)
        {
            await _newspaperService.IncreaseQuantityAsync(id, quantity);
            return NoContent();
        }
        [HttpPut("remove/{id}/{quantity}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> PutRemove(Guid id, int quantity)
        {
            await _newspaperService.DecreaseQuantityAsync(id, quantity);
            return NoContent();
        }
        [HttpDelete]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _newspaperService.DeleteAsync(id);
            return NoContent();
        }
    }
}
