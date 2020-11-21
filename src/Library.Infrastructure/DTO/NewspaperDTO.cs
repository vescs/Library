using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.DTO
{
    public class NewspaperDTO
    {
        public Guid Id { get; set; }
        public string Type { get; protected set; }
        public string Description { get; protected set; }
        public DateTime ReleaseDate { get; protected set; }
    }
}
