using Library.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Route("[controller]")]
    public class ControllerBase : Controller
    {
        protected readonly ICommandDispatcher CommandDispatcher;
        public ControllerBase(ICommandDispatcher commandDispatcher)
        {
            CommandDispatcher = commandDispatcher;
        }
        protected Guid UserId => User?.Identity?.IsAuthenticated == true ? Guid.Parse(User.Identity.Name) : Guid.Empty;
    }
}
