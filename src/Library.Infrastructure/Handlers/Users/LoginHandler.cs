using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Users;
using Library.Infrastructure.DTO;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Users
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IUserService _userService;
        public LoginHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(Login command)
        {
            await _userService.LoginAsync(command.Email, command.Password);
        }
    }
}
