using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Events
{
    public class CreateEvent : CreateCommandBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
