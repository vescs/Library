using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Newspapers
{
    public class UpdateNewspaper : ICommand
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
