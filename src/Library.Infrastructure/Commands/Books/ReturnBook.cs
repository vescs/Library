using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Books
{
    public class ReturnBook : AuthenticatedCommandBase
    {
        public Guid BookId { get; set; }
    }
}
