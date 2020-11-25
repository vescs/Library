using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.DTO
{
    public class NewspaperDetailsDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Quantity { get; set; }
        public int AvailableNewspapers { get; set; }
        public int LentNewspapers { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
