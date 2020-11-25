using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.DTO
{
    public class MovieDetailsDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public int Length { get; set; }
        public DateTime PremiereDate { get; set; }
        public int Quantity { get; set; }
        public int AvailableMovies { get; set; }
        public int LentMovies { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
