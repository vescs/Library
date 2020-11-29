using Library.Infrastructure.Commands;
using Library.Infrastructure.Commands.Users;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Handlers.Users
{
    public class RegisterHandler : ICommandHandler<Register>
    {
        private readonly IUserService _userService;
        public RegisterHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleAsync(Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Username, command.Password,
                command.FirstName, command.LastName, command.Role);
        }
    }
}
