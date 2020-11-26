using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Events
{
    public class AddTickets
    {
        public Guid EventId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public bool Seat { get; set; }
    }
}
