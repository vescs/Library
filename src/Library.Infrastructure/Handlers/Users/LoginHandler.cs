using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Users;
using Library.Infrastructure.DTO;
using Library.Infrastructure.Extentions;
using Library.Infrastructure.IServices;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Users
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _memoryCache;

        public LoginHandler(IUserService userService, IJwtHandler jwtHandler, IMemoryCache memoryCache)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
            _memoryCache = memoryCache;
        }

        public async Task HandleAsync(Login command)
        {
            await _userService.LoginAsync(command.Email, command.Password);
            var user = await _userService.GetUser(command.Email);
            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
            _memoryCache.SetJwt(command.Id, jwt); //id of token
        }
    }
}
