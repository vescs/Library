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
        private readonly IFluentHandler _fluentHandler;

        public LoginHandler(IUserService userService, IJwtHandler jwtHandler, 
            IMemoryCache memoryCache, IFluentHandler fluentHandler)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
            _memoryCache = memoryCache;
            _fluentHandler = fluentHandler;
        }

        public async Task HandleAsync(Login command)
            => await _fluentHandler
                .Run(async () => await _userService.LoginAsync(command.Email, command.Password))
                .Next()
                .Run(async () =>
                {
                    var user = await _userService.GetUser(command.Email);
                    var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
                    _memoryCache.SetJwt(command.Id, jwt); //id of token
                })
                .ExecuteAsync();


    }
}
