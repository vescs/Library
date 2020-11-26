using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.DTO
{
    public class EventDetailsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PurchasedTickets { get; set; }
        public int AvailableTickets { get; set; }
        public IEnumerable<TicketDTO> Tickets { get; set; }

    }
}
