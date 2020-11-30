using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Library.Infrastructure.Commands
{
    public class AuthenticatedCommandBase : IAuthenticatedCommand
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
