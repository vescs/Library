﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Commands.Books
{
    public class CreateBook : CreateCommandBase
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Pages { get; set; }
        public string PublishingHouse { get; set; }
        public DateTime PremiereDate { get; set; }
        public int Quantity { get; set; }
    }
}
