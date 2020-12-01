using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Events
{
    public class UpdateEvent : ICommand
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
