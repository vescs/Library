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
        private readonly IFluentHandler _fluentHandler;

        public RegisterHandler(IUserService userService, IFluentHandler fluentHandler)
        {
            _userService = userService;
            _fluentHandler = fluentHandler;
        }

        public async Task HandleAsync(Register command)
            => await _fluentHandler
                .Run(async () => await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Username, command.Password,
                command.FirstName, command.LastName, command.Role))
                .ExecuteAsync();

    }
}
