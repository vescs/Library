using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Newspapers
{
    public class RemoveNewspaper : ICommand
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
