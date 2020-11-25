using Library.Core.Models;
using Library.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class InMemoryBookRepository : IBookRepository
    {
        private static ISet<Book> _books = new HashSet<Book>();
        public async Task AddAsync(Book book)
        {
            _books.Add(book);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Book>> BrowseAsync(string title = "")
        {
            var books = _books.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(title))
            {
                books = _books.Where(x => x.Title.ToLowerInvariant().Contains(title.ToLowerInvariant()));
            }
            return await Task.FromResult(books);
        }
        public async Task<IEnumerable<Book>> BrowseAuthorsAsync(string author = "")
        {
            var books = _books.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(author))
            {
                books = _books.Where(x => x.Author.ToLowerInvariant().Contains(author.ToLowerInvariant()));
            }
            return await Task.FromResult(books);
        }
        public async Task<IEnumerable<Book>> BrowseHousesAsync(string house = "")
        {
            var books = _books.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(house))
            {
                books = _books.Where(x => x.PublishingHouse.ToLowerInvariant().Contains(house.ToLowerInvariant()));
            }
            return await Task.FromResult(books);
        }

        public async Task DeleteAsync(Guid id)
        {
            var book = _books.SingleOrDefault(x => x.Id == id);
            if(book == null)
            {
                return;
            }
            _books.Remove(book);
            await Task.CompletedTask;
        }

        public async Task<Book> GetAsync(Guid id)
        {
            return await Task.FromResult(_books.SingleOrDefault(x => x.Id == id));
        }

        public async Task UpdateAsync(Book book)
        {
            await Task.CompletedTask;
        }
    }
}
