using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Repositories
{
    public interface IBookRepository
    {
        Task AddAsync(Book book);
        Task<IEnumerable<Book>> BrowseAsync(string name = "");
        Task DeleteAsync(Guid id);
        Task<Book> GetAsync(Guid id);
        Task<Newspaper> GetAsync(string title);
        Task UpdateAsync(Book book);
    }
}
