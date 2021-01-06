﻿using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Exceptions
{
    public class ServiceException : LibraryException
    {
        public ServiceException()
        {

        }

        public ServiceException(string code) : base(code)
        {

        }

        public ServiceException(string message, params object[] args)
            : base(string.Empty, message, args)
        {

        }

        public ServiceException(string code, string message, params object[] args)
            : base(null, code, message, args)
        {

        }

        public ServiceException(Exception innerException, string message, params object[] args)
            : base(innerException, string.Empty, message, args)
        {

        }

        public ServiceException(Exception innerException, string code, string message, params object[] args)
            : base(code, string.Format(message, args), innerException)
        {

        }
    }
}
