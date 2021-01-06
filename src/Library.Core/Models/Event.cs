using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.Models
{
    public class Event : Entity
    {
        private ISet<Ticket> _tickets = new HashSet<Ticket>();
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public IEnumerable<Ticket> Tickets
        {
            get { return _tickets; }
            protected set { _tickets = new HashSet<Ticket>(value); }
        }
        public IEnumerable<Ticket> PurchasedTickets => Tickets.Where(x => x.Purchased);
        public IEnumerable<Ticket> AvailableTickets => Tickets.Where(x => !x.Purchased);
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected Event() { }

        public Event(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            Id = id;
            SetName(name);
            SetDescription(description);
            SetStartDate(startDate);
            SetEndDate(endDate);
            CreatedAt = DateTime.UtcNow;
            Update();
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException(DomainErrorCodes.InvalidName, 
                    $"Name can't be empty.");
            }
            if (name == Name)
            {
                return;
            }
            Name = name;
            Update();

        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new DomainException(DomainErrorCodes.InvalidDescription, 
                    $"Description can't be empty.");
            }
            if (description == Description)
            {
                return;
            }
            Description = description;
            Update();
        }

        public void SetStartDate(DateTime startDate)
        {
            if (startDate == DateTime.MinValue)
            {
                throw new DomainException(DomainErrorCodes.InvalidDate, 
                    "Enter proper date.");
            }
            if (startDate == StartDate)
            {
                return;
            }
            StartDate = startDate;
            Update();
        }

        public void SetEndDate(DateTime endDate)
        {
            if (endDate == DateTime.MinValue)
            {
                throw new DomainException(DomainErrorCodes.InvalidDate, 
                    "Enter proper date.");
            }
            if (endDate < StartDate)
            {
                throw new DomainException(DomainErrorCodes.InvalidDate, 
                    "Event can not finish before start.");
            }
            if(endDate == EndDate)
            {
                return;
            }
            EndDate = endDate;
            Update();
        }

        public void AddTickets(int amount, decimal price, bool seat)
        {
            if (amount <= 0)
            {
                throw new DomainException(DomainErrorCodes.InvalidQuantity, 
                    "Amount of tickets has to be greater than zero.");
            }
            if (price < 0)
            {
                throw new DomainException(DomainErrorCodes.InvalidPrice, 
                    "Price of tickets can not be negative number.");
            }
            for (int i = 0; i < amount; i++)
            {
                _tickets.Add(new Ticket(this, seat, price));
            }
            Update();
        }

        public void PurchaseTickets(User user, int amount, bool seat)
        {
            if (amount <= 0)
            {
                throw new DomainException(DomainErrorCodes.InvalidQuantity, 
                    "Amount of tickets has to be greater than zero.");
            }
            if (AvailableTickets.Count() < amount)
            {
                throw new DomainException(DomainErrorCodes.NotEnoughTickets, 
                    "There is less tickets than you want to buy.");
            }
            var tickets = AvailableTickets.Where(x => x.Seat == seat).Take(amount);
            if(tickets.Count() < amount)
            {
                throw new DomainException(DomainErrorCodes.NotEnoughTickets, 
                    "There is less tickets than you want to buy.");
            }
            foreach (var ticket in tickets)
            {
                ticket.Purchase(user);
            }
            Update();
        }

        public void CancelTickets(User user, int amount, bool seat)
        {
            if(amount <= 0)
            {
                throw new DomainException(DomainErrorCodes.InvalidQuantity, 
                    "Amount of tickets has to be greater than zero.");
            }
            var tickets = TicketsBoughtByUser(user).Where(x => x.Seat == seat).Take(amount);
            if (tickets.Count() < amount)
            {
                throw new DomainException(DomainErrorCodes.InvalidQuantity, 
                    "You want to cancel more tickets than you own.");
            }
            foreach (var ticket in tickets)
            {
                ticket.Cancel();
            }
            Update();
        }

        public IEnumerable<Ticket> TicketsBoughtByUser(User user)
        {
            return PurchasedTickets.Where(x => x.UserId == user.Id);
        }

        private void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
