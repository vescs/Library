using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.DTO
{
    public class TicketDTO
    {
        public Guid Id { get; set; }
        public bool Seat { get; set; }
        public decimal Price { get; set; }
        public string Username { get; set; }
        public bool Purchased { get; set; }
    }
}
