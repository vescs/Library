using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Tickets
{
    public class CancelTickets : AuthenticatedCommandBase
    {
        public Guid EventId { get; set; }
        public int Amount { get; set; }
        public bool Seat { get; set; }
    }
}
