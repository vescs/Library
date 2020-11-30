using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Movies
{
    public class ReturnMovie : AuthenticatedCommandBase
    {
        public Guid Id { get; set; }
    }
}
