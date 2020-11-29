using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Users;
using Library.Infrastructure.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
        {
            _userService = userService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Json(await _userService.GetUserInfoAsync(UserId));
        }
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]Register command)
        {
            await CommandDispatcher.DispatchAsync(command);
            return Created("/account", null);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));
    }
}
