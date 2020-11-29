using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Books
{
    public class UpdateBook : ICommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
