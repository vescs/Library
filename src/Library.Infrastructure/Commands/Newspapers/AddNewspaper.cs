using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Newspapers
{
    public class AddNewspaper : ICommand
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
