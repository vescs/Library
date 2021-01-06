using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Models
{
    public class LibraryException : Exception
    {
        public string Code { get; }

        protected LibraryException()
        {

        }

        protected LibraryException(string code)
        {
            Code = code;
        }

        protected LibraryException(string message, params object[] args) 
            : this(string.Empty, message, args)
        {
            
        }

        protected LibraryException(string code, string message, params object[] args) 
            : this(null, code, message, args)
        {
            
        }

        protected LibraryException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
           
        }

        protected LibraryException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
