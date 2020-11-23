using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IBookService _bookService;
        public DataInitializer(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task SeedAsync()
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(_bookService.CreateAsync(Guid.NewGuid(), $"Title {i}", $"Description {i}", 
                    $"Author {i}", i + 100, $"House {i}", DateTime.UtcNow.AddDays(-i)));
            }
            await Task.WhenAll(tasks);
        }
    }
}
