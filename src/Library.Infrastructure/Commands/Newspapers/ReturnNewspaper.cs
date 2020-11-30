using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Newspapers
{
    public class ReturnNewspaper : AuthenticatedCommandBase
    {
        public Guid Id { get; set; }
    }
}
