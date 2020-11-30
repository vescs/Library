using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Movies
{
    public class AddMovie : ICommand
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
