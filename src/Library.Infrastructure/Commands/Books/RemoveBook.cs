using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Books
{
    public class RemoveBook: ICommand
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
