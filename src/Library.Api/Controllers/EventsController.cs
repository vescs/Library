﻿using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Events;
using Library.Infrastructure.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService, ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
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

        [HttpDelete]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _eventService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Post([FromBody]CreateEvent command)
        {
            await DispatchAsync(command);
            return Created($"/events/{command.Id}", null);
        }

        [HttpPut]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put([FromBody]UpdateEvent command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [HttpPut("addtickets")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Put([FromBody]AddTickets command)
        {
            await DispatchAsync(command);
            return NoContent();
        }


    }
}
