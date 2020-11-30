using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Books
{
    public class LendBook : AuthenticatedCommandBase
    {
        public Guid Id { get; set; }
    }
}
