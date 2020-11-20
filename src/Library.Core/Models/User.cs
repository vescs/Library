using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Models
{
    public class User : Entity
    {
        private List<string> _roles = new List<string>()
        {
            "user",
            "admin"
        };
        public string Username { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Role { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected User() { }
        public User(Guid id, string username, string email, string password, string role, string firstName, string lastName)
        {
            Id = id;
            SetUsername(username);
            SetEmail(email);
            SetPassword(password);
            SetRole(role);
            SetFirstName(firstName);
            SetLastName(lastName);
            CreatedAt = DateTime.UtcNow;
            Update();
        }
        public void SetUsername(string username)
        {
            if(string.IsNullOrWhiteSpace(username))
            {
                throw new Exception($"Username can't be empty.");
            }
            if(username == Username)
            {
                return;
            }
            Username = username;
            Update();
        }
        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception($"Email can't be empty.");
            }
            if(email == Email)
            {
                return;
            }
            Email = email;
            Update();
        }
        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 5)
            {
                throw new Exception($"Given password is invalid. Please enter password longer than 4 characters.");
            }
            if(password == Password)
            {
                return;
            }
            Password = password;
            Update();
        }
        public void SetRole(string role)
        {
            if (!_roles.Contains(role))
            {
                throw new Exception("Given role does not exist.");
            }
            if(role == Role)
            {
                return;
            }
            Role = role;
            Update();
        }
        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            { 
                throw new Exception($"First name can't be empty.");
            }
            if (firstName == FirstName)
            {
                return;
            }
            FirstName = firstName;
            Update();
        }
        public void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new Exception($"Last name can't be empty.");
            }
            if (lastName == LastName)
            {
                return;
            }
            LastName = lastName;
            Update();
        }
        private void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
