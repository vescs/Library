using Library.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Route("[controller]")]
    public class ControllerBase : Controller
    {
        protected readonly ICommandDispatcher CommandDispatcher;
        protected Guid UserId => User?.Identity?.IsAuthenticated == true ? Guid.Parse(User.Identity.Name) : Guid.Empty;
        public ControllerBase(ICommandDispatcher commandDispatcher)
        {
            CommandDispatcher = commandDispatcher;
        }
        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if(command is IAuthenticatedCommand _command)
            {
                _command.UserId = UserId;
            }
            await CommandDispatcher.DispatchAsync(command);
        }
    }
}
