using Library.Infrastructure.Commands.Users;
using Library.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Username, command.Password, 
                command.FirstName, command.LastName, command.Role);
            return Created("/account", null);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));
    }
}
