using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.IServices
{
    public interface IEncrypter
    {
        public string GetHash(string password, string salt);
        public string GetSalt(string password);
    }
}
