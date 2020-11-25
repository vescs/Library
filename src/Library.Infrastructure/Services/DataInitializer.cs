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
        private readonly IEventService _eventService;
        private readonly INewspaperService _newspaperService;
        private readonly IMovieService _movieService;
        public DataInitializer(IBookService bookService, IEventService eventService, 
            INewspaperService newspaperService, IMovieService movieService)
        {
            _bookService = bookService;
            _eventService = eventService;
            _movieService = movieService;
            _newspaperService = newspaperService;
        }
        public async Task SeedAsync()
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(_bookService.CreateAsync(Guid.NewGuid(), $"Title {i}", $"Description {i}", 
                    $"Author {i}", i + 100, $"House {i}", 5, DateTime.UtcNow.AddDays(-i)));
                tasks.Add(_eventService.CreateAsync(Guid.NewGuid(), $"Name {i}", $"Description {i}",
                    DateTime.UtcNow.AddDays(-i), DateTime.UtcNow.AddDays(i)));
                tasks.Add(_movieService.CreateAsync(Guid.NewGuid(), $"Title {i}", $"Description {i}",
                    $"Director {i}", i + 110, DateTime.UtcNow.AddDays(-i)));
                tasks.Add(_newspaperService.CreateAsync(Guid.NewGuid(), $"Title {i}", $"Description {i}",
                    $"Type {i}", 10, DateTime.UtcNow.AddDays(-i)));
            }
            await Task.WhenAll(tasks);
        }
    }
}
