using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Models
{
    public class Ticket : Entity
    {
        public Guid EventId { get; protected set; }
        public Guid? UserId { get; protected set; }
        public bool Seating { get; protected set; }
        public decimal Price { get; protected set; }
        public string Username { get; protected set; }
        public DateTime? PurchasedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public bool Purchased => UserId.HasValue;
        protected Ticket() { }
        public Ticket(Event @event, bool seat, decimal price)
        {
            EventId = @event.Id;
            Seating = seat;
            Price = price;
        }
        public void Purchase(User user)
        {
            //todo
        }
        public void Cancel(User user)
        {
            //todo
        }
    }
}
