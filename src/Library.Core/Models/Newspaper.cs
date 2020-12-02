using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.Models
{
    public class Newspaper : Entity
    {
        private ISet<User> _users = new HashSet<User>();
        public string Title { get; protected set; }
        public string Type { get; protected set; }
        public string Description { get; protected set; }
        public DateTime ReleaseDate { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public IEnumerable<User> Users
        {
            get { return _users; }
            protected set { _users = new HashSet<User>(value); }
        }
        public int Quantity { get; protected set; }
        public int AvailableNewspapers => Quantity - _users.Count;
        public int LentNewspapers => _users.Count;
        protected Newspaper() { }
        public Newspaper(Guid id, string title, string description, string type, int quantity, DateTime releaseDate)
        {
            Id = id;
            SetTitle(title);
            SetDescription(description);
            SetReleaseDate(releaseDate);
            SetType(type);
            SetQuantity(quantity);
            CreatedAt = DateTime.UtcNow;
            Update();
        }
        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new Exception("Title can not be empty.");
            }
            if (title == Title)
            {
                return;
            }
            Title = title;
            Update();
        }
        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new Exception("Description can not be empty.");
            }
            if (description == Description)
            {
                return;
            }
            Description = description;
            Update();
        }
        
        public void SetReleaseDate(DateTime releaseDate)
        {
            if (releaseDate == DateTime.MinValue)
            {
                throw new Exception("Enter proper date.");
            }
            if (releaseDate == ReleaseDate)
            {
                return;
            }
            ReleaseDate = releaseDate;
            Update();
        }
        public void SetType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new Exception("Type can not be empty.");
            }
            if (type == Type)
            {
                return;
            }
            Type = type;
            Update();
        }
        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new Exception("Quantity has to be greater than zero.");
            }
            if (quantity == Quantity)
            {
                return;
            }
            Quantity = quantity;
            Update();
        }
        public void IncreaseQuantity(int quantity)
        {
            if(quantity <= 0)
            {
                throw new Exception("Quantity has to be greater than zero.");
            }
            Quantity += quantity;
            Update();
        }
        public void DecreaseQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new Exception("Quantity has to be greater than zero.");
            }
            if (AvailableNewspapers < quantity)
            {
                throw new Exception("Quantity of newspapers you want to cancel is bigger than quantity of newspapers you can cancel.");
            }

            Quantity -= quantity;
            Update();
        }
        public void Lend(User user)
        {
            if(_users.Count >= Quantity)
            {
                throw new Exception("There are no available newspapers.");
            }
            if (_users.Contains(user))
            {
                throw new Exception("You already own this newspaper.");
            }
            _users.Add(user);
            Update();
        }
        public void Return(User user)
        {
            if (!_users.Any(x => x.Id == user.Id))
            {
                throw new Exception("There is nothing to return.");
            }
            _users.Remove(_users.FirstOrDefault(x => x.Id == user.Id));
            Update();
        }
        private void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
