using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Newspapers
{
    public class CreateNewspaper : CreateCommandBase
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Quantity { get; set; }
    }
}
