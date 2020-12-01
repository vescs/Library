using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Library.Infrastructure.Commands
{
    public abstract class CreateCommandBase : ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
