using Library.Core.Models;
using Library.Core.Repositories;
using Library.Infrastructure.Extentions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<User> Users => _database.GetCollection<User>("Users");

        public MongoUserRepository(IConfiguration configuration)
        {
            _database = configuration.GetMongoDatabase();
        }

        public async Task AddAsync(User user)
            => await Users.InsertOneAsync(user);

        public async Task<IEnumerable<User>> BrowseAsync(string username = "")
        {
            var list = await Users.AsQueryable()
                .Where(x => x.Username.ToLowerInvariant()
                .Contains(username.ToLowerInvariant()))
                .ToListAsync();
            return list.OrderBy(x => x.Username).AsEnumerable();
        }

        public async Task DeleteAsync(Guid id)
            => await Users.DeleteOneAsync(x => x.Id == id);

        public async Task<User> GetAsync(Guid id)
            => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string email)
            => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);

        public async Task UpdateAsync(User user)
            => await Users.ReplaceOneAsync(x => x.Id == user.Id, user);
    }
}
