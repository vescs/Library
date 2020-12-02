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
    public class MongoEventRepository : IEventRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<Event> Events => _database.GetCollection<Event>("Events");

        public MongoEventRepository(IConfiguration configuration)
        {
            _database = configuration.GetMongoDatabase();
        }

        public async Task AddAsync(Event @event)
        {
            await Events.InsertOneAsync(@event);
        }

        public async Task<IEnumerable<Event>> BrowseAsync(string name = "")
        {
            var list = await Events.AsQueryable()
                .Where(x => x.Name.ToLowerInvariant()
                .Contains(name.ToLowerInvariant()))
                .ToListAsync();
            return list.OrderBy(x => x.Name).AsEnumerable();
        }

        public async Task DeleteAsync(Guid id)
            => await Events.DeleteOneAsync(x => x.Id == id);

        public async Task<Event> GetAsync(Guid id)
            => await Events.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Event> GetAsync(string name)
            => await Events.AsQueryable().FirstOrDefaultAsync(x => x.Name == name);

        public async Task UpdateAsync(Event @event)
            => await Events.ReplaceOneAsync(x => x.Id == @event.Id, @event);
    }
}
