using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        public Guid UserId { get; set; }
    }
}
