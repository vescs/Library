using Library.Infrastructure.Commands.Events;
using Library.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Route("[controller]")]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string name = "")
        {
            var events = await _eventService.BrowseAsync(name);
            return Json(events);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var @event = await _eventService.GetAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return Json(@event);
        }
        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetTitle(string title)
        {
            var @event = await _eventService.GetAsync(title);
            if (@event == null)
            {
                return NotFound();
            }
            return Json(@event);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateEvent command)
        {
            command.Id = Guid.NewGuid();
            await _eventService.CreateAsync(command.Id, command.Name, command.Description, 
                command.StartDate, command.EndDate);
            return Created($"/events/{command.Id}", null);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateEvent command, Guid id)
        {
            var @event = await _eventService.GetAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            await _eventService.UpdateAsync(id, command.Description);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var @event = await _eventService.GetAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            await _eventService.DeleteAsync(id);
            return NoContent();
        }
    }
}
