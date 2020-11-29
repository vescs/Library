using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Repositories
{
    public interface IBookRepository : IRepository
    {
        Task AddAsync(Book book);
        Task<IEnumerable<Book>> BrowseAsync(string title = "");
        Task<IEnumerable<Book>> BrowseAuthorsAsync(string author = "");
        Task<IEnumerable<Book>> BrowseHousesAsync(string house = "");
        Task DeleteAsync(Guid id);
        Task<Book> GetAsync(Guid id);
        Task UpdateAsync(Book book);
    }
}
