using Library.Core.Models;
using Library.Core.Repositories;
using Library.Infrastructure.Extentions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class MongoNewspaperRepository : INewspaperRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<Newspaper> Newspapers => _database.GetCollection<Newspaper>("Newspapers");

        public MongoNewspaperRepository(IConfiguration configuration)
        {
            _database = configuration.GetMongoDatabase();
        }

        public async Task AddAsync(Newspaper newspaper)
            => await Newspapers.InsertOneAsync(newspaper);

        public async Task<IEnumerable<Newspaper>> BrowseAsync(string title = "")
        {
            var list = await Newspapers.AsQueryable()
                .Where(x => x.Title.ToLowerInvariant()
                .Contains(title.ToLowerInvariant()))
                .ToListAsync();
            return list.OrderBy(x => x.Title).AsEnumerable();
        }

        public async Task DeleteAsync(Guid id)
            => await Newspapers.DeleteOneAsync(x => x.Id == id);

        public async Task<Newspaper> GetAsync(Guid id)
            => await Newspapers.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(Newspaper newspaper)
            => await Newspapers.ReplaceOneAsync(x => x.Id == newspaper.Id, newspaper);
    }
}
