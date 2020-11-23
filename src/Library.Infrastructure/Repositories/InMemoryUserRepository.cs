using Library.Core.Models;
using Library.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>()
        {
            new User(Guid.NewGuid(), "User", "user@user.com", "secret", "user", "User", "User"),
            new User(Guid.NewGuid(), "Admin", "admin@admin.com", "secret", "admin", "Admin", "Admin"),
        };
        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<User>> BrowseAsync(string username = "")
        {
            var users = _users.Where(x => x.Username.ToLowerInvariant().Contains(username.ToLowerInvariant()));
            return await Task.FromResult(users);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = _users.SingleOrDefault(x => x.Id == id);
            _users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = _users.SingleOrDefault(x => x.Id == id);
            return await Task.FromResult(user);
        }

        public async Task<User> GetAsync(string email)
        {
            var user = _users.SingleOrDefault(x => x.Email == email);
            return await Task.FromResult(user);
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }
    }
}
