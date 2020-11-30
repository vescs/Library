using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Books
{
    public class AddBook : ICommand
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
