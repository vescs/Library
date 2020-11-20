using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<IEnumerable<User>> BrowseAsync(string username = "");
        Task DeleteAsync(Guid id);
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task UpdateAsync(User user);
    }
}
