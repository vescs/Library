using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
        public string Salt { get; protected set; }
        public string Role { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected User() { }

        public User(Guid id, string username, string email, string password, string salt, string role, string firstName, string lastName)
        {
            Id = id;
            SetUsername(username);
            SetEmail(email);
            SetPassword(password, salt);
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
                throw new DomainException(DomainErrorCodes.InvalidUsername, 
                    $"Username can't be empty.");
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
            Regex mailRegex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                + "@"
                                + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException(DomainErrorCodes.InvalidEmail, 
                    $"Email can't be empty.");
            }
            if (!mailRegex.IsMatch(email))
            {
                throw new DomainException(DomainErrorCodes.InvalidEmail, 
                    $"Invalid email. Please enter correct one.");
            }
            if(email == Email)
            {
                return;
            }
            Email = email.ToLowerInvariant();
            Update();
        }

        public void SetPassword(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 5)
            {
                throw new DomainException(DomainErrorCodes.InvalidPassword, 
                    $"Given password is invalid. Please enter password longer than 4 characters.");
            }
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new DomainException(DomainErrorCodes.InvalidPassword, 
                    "Salt can not be empty.");
            }
            if (password == Password)
            {
                return;
            }
            Password = password;
            Salt = salt;
            Update();
        }

        public void SetRole(string role)
        {
            role = role.ToLowerInvariant();
            if (!_roles.Contains(role))
            {
                throw new DomainException(DomainErrorCodes.InvalidRole,
                    "Given role does not exist.");
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
                throw new DomainException(DomainErrorCodes.InvalidFirstName, 
                    $"First name can't be empty.");
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
                throw new DomainException(DomainErrorCodes.InvalidLastName, 
                    $"Last name can't be empty.");
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

        public override bool Equals(object obj)
        {
            User tempUser = obj as User;
            if(tempUser == null)
            {
                return false;
            }
            return tempUser.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
