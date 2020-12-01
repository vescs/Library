﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Users
{
    public class Login : CreateCommandBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
