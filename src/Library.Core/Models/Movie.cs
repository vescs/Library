using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Models
{
    public class Movie : Entity
    {
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public string Director { get; protected set; }
        public int Length { get; protected set; }
        public DateTime PremiereDate { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        protected Movie() { }
        public Movie(Guid id, string title, string description, string director, int length, DateTime premiereDate)
        {
            Id = id;
            SetTitle(title);
            SetDescription(description);
            SetLength(length);
            SetPremiereDate(premiereDate);
            CreatedAt = DateTime.UtcNow;
            Update();
        }
        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new Exception("Title can not be empty.");
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
                throw new Exception("Description can not be empty.");
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
                throw new Exception("Lenght has to be greater than zero.");
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
                throw new Exception("Enter proper date.");
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
                throw new Exception("Director's name is missing.");
            }
            if (director == Director)
            {
                return;
            }
            Director = director;
            Update();
        }
        private void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
