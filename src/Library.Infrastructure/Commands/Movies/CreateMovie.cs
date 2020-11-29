using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Movies
{
    public class CreateMovie : ICommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public int Length { get; set; }
        public DateTime PremiereDate { get; set; }
        public int Quantity { get; set; }
    }
}
