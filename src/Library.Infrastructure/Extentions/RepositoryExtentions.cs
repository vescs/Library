using Library.Core.Models;
using Library.Core.Repositories;
using Library.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Extentions
{
    public static class RepositoryExtentions
    {
        public static async Task<Book> SafeGetAsync(this IBookRepository bookRepository, Guid id)
        {
            var book = await bookRepository.GetAsync(id);
            if (book == null)
            {
                throw new ServiceException(ServiceErrorCodes.DoesNotExist, 
                    $"Book with id '{id}' does not exist.");
            }
            return book;
        }
        public static async Task<Event> SafeGetAsync(this IEventRepository eventRepository, Guid id)
        {
            var @event = await eventRepository.GetAsync(id);
            if (@event == null)
            {
                throw new ServiceException(ServiceErrorCodes.DoesNotExist, 
                    $"Event with id '{id}' does not exist.");
            }
            return @event;
        }
        public static async Task<Movie> SafeGetAsync(this IMovieRepository movieRepository, Guid id)
        {
            var movie = await movieRepository.GetAsync(id);
            if (movie == null)
            {
                throw new ServiceException(ServiceErrorCodes.DoesNotExist, 
                    $"Movie with id '{id}' does not exist.");
            }
            return movie;
        }
        public static async Task<Newspaper> SafeGetAsync(this INewspaperRepository newspaperRepository, Guid id)
        {
            var newspaper = await newspaperRepository.GetAsync(id);
            if (newspaper == null)
            {
                throw new ServiceException(ServiceErrorCodes.DoesNotExist, 
                    $"Newspaper with id '{id}' does not exist.");
            }
            return newspaper;
        }
        public static async Task<Event> SafeGetAsync(this IEventRepository eventRepository, string name)
        {
            var @event = await eventRepository.GetAsync(name);
            if (@event == null)
            {
                throw new ServiceException(ServiceErrorCodes.DoesNotExist, 
                    $"Event with name '{name}' does not exist.");
            }
            return @event;
        }
        public static async Task<User> SafeGetAsync(this IUserRepository userRepository, Guid id)
        {
            var user = await userRepository.GetAsync(id);
            if (user == null)
            {
                throw new ServiceException(ServiceErrorCodes.DoesNotExist, 
                    $"User with id '{id}' does not exist.");
            }
            return user;
        }
        public static async Task<User> SafeGetAsync(this IUserRepository userRepository, string email)
        {
            var user = await userRepository.GetAsync(email);
            if (user == null)
            {
                throw new ServiceException(ServiceErrorCodes.InvalidCredentials, 
                    "Invalid credentials");
            }
            return user;
        }
    }
}
