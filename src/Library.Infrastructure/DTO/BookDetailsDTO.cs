using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.DTO
{
    public class BookDetailsDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Pages { get; set; }
        public string PublishingHouse { get; set; }
        public DateTime PremiereDate { get; set; }
        public int Quantity { get; set; }
        public int AvailableBooks { get; set; }
        public int LentBooks { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
    }
}
