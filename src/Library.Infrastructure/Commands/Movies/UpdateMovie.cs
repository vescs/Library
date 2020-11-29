using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Movies
{
    public class UpdateMovie : ICommand
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
