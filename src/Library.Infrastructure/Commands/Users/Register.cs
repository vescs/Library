using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Users
{
    public class Register : CreateCommandBase
    {
        public string Role { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
