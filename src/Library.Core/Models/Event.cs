using System;
using System.Collections.Generic;
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
        public IEnumerable<Ticket> Tickets { get { return _tickets; } }
        //available tickets and purchased - later
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
                throw new Exception($"Name can't be empty.");
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
                throw new Exception($"Description can't be empty.");
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
                throw new Exception("Enter proper date.");
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
                throw new Exception("Enter proper date.");
            }
            if (endDate < StartDate)
            {
                throw new Exception("Event can not finish before start.");
            }
            if(endDate == EndDate)
            {
                return;
            }
            EndDate = endDate;
            Update();
        }
        public void AddTickets()
        {
            //todo
        }
        //todo tickets bought by user
        private void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
