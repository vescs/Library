using Library.Core.Repositories;
using Library.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.IServices
{
    public interface IBookService
    {
        Task CreateAsync(Guid id, string title, string description, string author, 
            int pages, string publishingHouse, DateTime premiereDate);
        Task<BookDTO> GetAsync(Guid id);
        Task<BookDTO> GetAsync(string title);
        Task<IEnumerable<BookDTO>> BrowseAsync(string title = "");
        Task<IEnumerable<BookDTO>> BrowseAuthorsAsync(string author = "");
        Task<IEnumerable<BookDTO>> BrowseHousesAsync(string house = "");
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, string title, string description);
    }
}
