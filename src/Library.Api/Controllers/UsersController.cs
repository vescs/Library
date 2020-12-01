using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Users;
using Library.Infrastructure.Extentions;
using Library.Infrastructure.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMemoryCache _memoryCache;

        public UsersController(IUserService userService, IMemoryCache memoryCache, ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
        {
            _userService = userService;
            _memoryCache = memoryCache;
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
            await DispatchAsync(command);
            return Created("/account", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
        {
            command.Id = Guid.NewGuid();
            await DispatchAsync(command);
            var jwt = _memoryCache.GetJwt(command.Id);
            return Json(jwt);
        }
    }
}
