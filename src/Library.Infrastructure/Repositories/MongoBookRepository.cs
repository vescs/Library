using Library.Core.Models;
using Library.Core.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Library.Infrastructure.Extentions;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Linq;

namespace Library.Infrastructure.Repositories
{
    public class MongoBookRepository : IBookRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<Book> Books => _database.GetCollection<Book>("Books");


        public MongoBookRepository(IConfiguration configuration)
        {
             _database = configuration.GetMongoDatabase();
        }

        public async Task AddAsync(Book book)
            => await Books.InsertOneAsync(book);

        public async Task<IEnumerable<Book>> BrowseAsync(string title = "")
        {
            if (title == null) title = "";
            var list = await Books.AsQueryable()
                .Where(x => x.Title.ToLowerInvariant()
                .Contains(title.ToLowerInvariant()))
                .ToListAsync();
            return list.OrderBy(x => x.Title).AsEnumerable();
        }

        public async Task<IEnumerable<Book>> BrowseAuthorsAsync(string author = "")
        {
            var list = await Books.AsQueryable()
                .Where(x => x.Author.ToLowerInvariant()
                .Contains(author.ToLowerInvariant()))
                .ToListAsync();
            return list.OrderBy(x => x.Title).AsEnumerable();
        }

        public async Task<IEnumerable<Book>> BrowseHousesAsync(string house = "")
        {
            var list = await Books.AsQueryable().
                Where(x => x.PublishingHouse.ToLowerInvariant()
                .Contains(house.ToLowerInvariant()))
                .ToListAsync();
            return list.OrderBy(x => x.Title).AsEnumerable();
        }

        public async Task DeleteAsync(Guid id)
            => await Books.DeleteOneAsync(x => x.Id == id);

        public async Task<Book> GetAsync(Guid id)
            => await Books.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(Book book)
            => await Books.ReplaceOneAsync(x => x.Id == book.Id, book);
    }
}
