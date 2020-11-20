using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Models
{
    public class Book : Entity
    {
        public string Title { get; protected set; }
        public string Author { get; protected set; }
        public string Description { get; protected set; }
        public int Pages { get; protected set; }
        public string PublishingHouse { get; protected set; }
        public DateTime PremiereDate { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        protected Book() { }
        public Book(Guid id, string title, string description, string author, int pages, string publishingHouse, DateTime premiereDate)
        {
            Id = id;
            SetTitle(title);
            SetDescription(description);
            SetAuthor(author);
            SetPages(pages);
            SetPremiereDate(premiereDate);
            SetPublishingHouse(publishingHouse);
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
        public void SetPages(int pages)
        {
            if (pages <= 0)
            {
                throw new Exception("Pages number have to be greater than zero.");
            }
            if (pages == Pages)
            {
                return;
            }
            Pages = pages;
            Update();
        }
        public void SetPremiereDate(DateTime premiereDate)
        {
            if (premiereDate == DateTime.MinValue)
            {
                throw new Exception("Enter proper date.");
            }
            if (premiereDate == PremiereDate)
            {
                return;
            }
            PremiereDate = premiereDate;
            Update();
        }
        public void SetAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new Exception("Author's name is missing.");
            }
            if (author == Author)
            {
                return;
            }
            Author = author;
            Update();
        }
        public void SetPublishingHouse(string publishingHouse)
        {
            if (string.IsNullOrWhiteSpace(publishingHouse))
            {
                throw new Exception("Publishing house can not be empty.");
            }
            if (publishingHouse == PublishingHouse)
            {
                return;
            }
            PublishingHouse = publishingHouse;
            Update();
        }
        private void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
