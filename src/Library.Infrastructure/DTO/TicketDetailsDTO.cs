using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.DTO
{
    public class TicketDetailsDTO
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public bool Seat { get; set; }
        public string Username { get; set; }
        public bool Purchased { get; set; }
        public DateTime? PurchasedAt { get; set; }
    }
}
