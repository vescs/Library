using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Models
{
    public class Newspaper : Entity
    {
        public string Title { get; protected set; }
        public string Type { get; protected set; }
        public string Description { get; protected set; }
        public DateTime ReleaseDate { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        protected Newspaper() { }
        public Newspaper(Guid id, string title, string description, string type, DateTime releaseDate)
        {
            Id = id;
            SetTitle(title);
            SetDescription(description);
            SetReleaseDate(releaseDate);
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
        private void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
