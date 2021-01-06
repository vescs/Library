using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Exceptions
{
    public static class ServiceErrorCodes
    {
        public static string InvalidCredentials => "invalid_credentials";
        public static string AlreadyExists => "already_exists";
        public static string DoesNotExist => "does_not_exist";
    }
}
