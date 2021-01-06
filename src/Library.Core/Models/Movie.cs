using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.Models
{
    public class Movie : Entity
    {
        private ISet<User> _users = new HashSet<User>();
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public string Director { get; protected set; }
        public int Length { get; protected set; }
        public DateTime PremiereDate { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public IEnumerable<User> Users
        {
            get { return _users; }
            protected set { _users = new HashSet<User>(value); }
        }
        public int Quantity { get; protected set; }
        public int AvailableMovies => Quantity - _users.Count;
        public int LentMovies => _users.Count;

        protected Movie() { }

        public Movie(Guid id, string title, string description, string director, int length, int quantity, DateTime premiereDate)
        {
            Id = id;
            SetTitle(title);
            SetDescription(description);
            SetLength(length);
            SetDirector(director);
            SetPremiereDate(premiereDate);
            SetQuantity(quantity);
            CreatedAt = DateTime.UtcNow;
            Update();
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new DomainException(DomainErrorCodes.InvalidTitle, 
                    "Title can not be empty.");
            }
            if(title == Title)
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
                throw new DomainException(DomainErrorCodes.InvalidDescription, 
                    "Description can not be empty.");
            }
            if (description == Description)
            {
                return;
            }
            Description = description;
            Update();
        }

        public void SetLength(int length)
        {
            if(length <= 0)
            {
                throw new DomainException(DomainErrorCodes.InvalidLength, 
                    "Lenght has to be greater than zero.");
            }
            if(length == Length)
            {
                return;
            }
            Length = length;
            Update();
        }

        public void SetPremiereDate(DateTime premiereDate)
        {
            if(premiereDate == DateTime.MinValue)
            {
                throw new DomainException(DomainErrorCodes.InvalidDate, 
                    "Enter proper date.");
            }
            if(premiereDate == PremiereDate)
            {
                return;
            }
            PremiereDate = premiereDate;
            Update();
        }

        public void SetDirector(string director)
        {
            if (string.IsNullOrWhiteSpace(director))
            {
                throw new DomainException(DomainErrorCodes.InvalidDirector, 
                    "Director's name is missing.");
            }
            if (director == Director)
            {
                return;
            }
            Director = director;
            Update();
        }

        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new DomainException(DomainErrorCodes.InvalidQuantity, 
                    "Quantity has to be greater than zero.");
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
            if (quantity <= 0)
            {
                throw new DomainException(DomainErrorCodes.InvalidQuantity, 
                    "Quantity has to be greater than zero.");
            }
            Quantity += quantity;
            Update();
        }

        public void DecreaseQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new DomainException(DomainErrorCodes.InvalidQuantity,
                    "Quantity has to be greater than zero.");
            }
            if (AvailableMovies < quantity)
            {
                throw new DomainException(DomainErrorCodes.InvalidQuantity, 
                    "Quantity of movies you want to cancel is bigger than quantity of movies you can cancel.");
            }
            Quantity -= quantity;

            Update();
        }

        public void Lend(User user)
        {
            if (_users.Count >= Quantity)
            {
                throw new DomainException(DomainErrorCodes.NoAvailableMovies, 
                    "There are no available movies.");
            }
            if (_users.Contains(user))
            {
                throw new DomainException(DomainErrorCodes.MovieAlreadyOwned, 
                    "You already own this movie.");
            }
            _users.Add(user);
            Update();
        }

        public void Return(User user)
        {
            if (!_users.Contains(user))
            {
                throw new DomainException(DomainErrorCodes.NotOwned, 
                    "There is nothing to return.");
            }
            _users.Remove(user);
            Update();
        }

        private void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
