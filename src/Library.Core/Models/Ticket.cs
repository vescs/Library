using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Models
{
    public class Ticket : Entity
    {
        public Guid EventId { get; protected set; }
        public Guid? UserId { get; protected set; }
        public bool Seat { get; protected set; }
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
            Seat = seat;
            Price = price;
            CreatedAt = DateTime.UtcNow;
            Update();
        }
        public void Purchase(User user)
        {
            if (Purchased)
            {
                throw new Exception($"Ticket was already purchased by: {Username}.");
            }
            UserId = user.Id;
            Username = user.Username;
            PurchasedAt = DateTime.UtcNow;
            Update();
        }
        public void Cancel(User user)
        {
            if (!Purchased)
            {
                throw new Exception($"Ticket wasn't purchased.");
            }
            UserId = null;
            Username = null;
            PurchasedAt = null;
            Update();
        }
        private void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
