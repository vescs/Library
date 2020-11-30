using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Newspapers
{
    public class LendNewspaper : AuthenticatedCommandBase
    {
        public Guid Id { get; set; }
    }
}
