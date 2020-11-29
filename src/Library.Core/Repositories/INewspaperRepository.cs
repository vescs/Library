using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Repositories
{
    public interface INewspaperRepository : IRepository
    {
        Task AddAsync(Newspaper newspaper);
        Task<IEnumerable<Newspaper>> BrowseAsync(string title = "");
        Task DeleteAsync(Guid id);
        Task<Newspaper> GetAsync(Guid id);
        Task UpdateAsync(Newspaper newspaper);
    }
}
