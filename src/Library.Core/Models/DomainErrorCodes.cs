using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Models
{
    public static class DomainErrorCodes
    {
        public static string InvalidEmail => "invalid_email";
        public static string InvalidPassword => "invalid_password";
        public static string InvalidRole => "invalid_role";
        public static string InvalidUsername => "invalid_username";
        public static string InvalidFirstName => "invalid_first_name";
        public static string InvalidLastName => "invalid_last_name";
    }
}
