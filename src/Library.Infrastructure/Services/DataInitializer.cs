﻿using Library.Infrastructure.IServices;
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
        private readonly IUserService _userService;

        public DataInitializer(IBookService bookService, IEventService eventService, 
            INewspaperService newspaperService, IMovieService movieService, IUserService userService)
        {
            _bookService = bookService;
            _eventService = eventService;
            _movieService = movieService;
            _newspaperService = newspaperService;
            _userService = userService;
        }

        public async Task SeedAsync()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "user@dx.com", "Name", "string", "FirstName", "LastName"));
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "admin@dx.com", "Name", "string", "FirstName", "LastName", "admin"));
            for (int i = 1; i <= 5; i++)
            {
                Guid id = Guid.NewGuid();
                await _eventService.CreateAsync(id, $"Name {i}", $"Description {i}",
                DateTime.UtcNow.AddDays(i), DateTime.UtcNow.AddDays(i).AddHours(i));
                await _eventService.AddTicketsAsync(id, 5, 15, true);
                await _eventService.AddTicketsAsync(id, 5, 10, false);
                tasks.Add(_bookService.CreateAsync(Guid.NewGuid(), $"Title {i}", $"Description {i}", 
                    $"Author {i}", i + 100, $"House {i}", 5, DateTime.UtcNow.AddDays(-i)));
                tasks.Add(_movieService.CreateAsync(Guid.NewGuid(), $"Title {i}", $"Description {i}",
                    $"Director {i}", i + 110, 15, DateTime.UtcNow.AddDays(-i)));
                tasks.Add(_newspaperService.CreateAsync(Guid.NewGuid(), $"Title {i}", $"Description {i}",
                    $"Type {i}", 10, DateTime.UtcNow.AddDays(-i)));
            }
            await Task.WhenAll(tasks);
        }
    }
}
