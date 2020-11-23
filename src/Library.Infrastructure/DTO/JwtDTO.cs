using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.DTO
{
    public class JwtDTO
    {
        public string Token { get; set; }
        public long Expires { get; set; }
    }
}
