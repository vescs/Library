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

        [HttpDelete]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _newspaperService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Post([FromBody]CreateNewspaper command)
        {
            await DispatchAsync(command);
            return Created($"newspapers/{command.Id}", null);
        }

        [HttpPut("update")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put([FromBody]UpdateNewspaper command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [HttpPut("lend")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody]LendNewspaper command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [HttpPut("return")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody]ReturnNewspaper command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [HttpPut("add")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put([FromBody]AddNewspaper command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [HttpPut("remove")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put([FromBody]RemoveNewspaper command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

    }
}
