using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Repositories
{
    public interface INewspaperRepository
    {
        Task AddAsync(Newspaper newspaper);
        Task<IEnumerable<Newspaper>> BrowseAsync(string name = "");
        Task DeleteAsync(Guid id);
        Task<Newspaper> GetAsync(Guid id);
        Task<Newspaper> GetAsync(string title);
        Task UpdateAsync(Newspaper newspaper);
    }
}
