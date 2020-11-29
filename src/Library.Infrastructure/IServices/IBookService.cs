using Library.Core.Repositories;
using Library.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.IServices
{
    public interface IBookService : IService
    {
        Task CreateAsync(Guid id, string title, string description, string author, 
            int pages, string publishingHouse, int quantity, DateTime premiereDate);
        Task<BookDetailsDTO> GetAsync(Guid id);
        Task<IEnumerable<BookDTO>> BrowseAsync(string title = "");
        Task<IEnumerable<BookDTO>> BrowseAuthorsAsync(string author = "");
        Task<IEnumerable<BookDTO>> BrowseHousesAsync(string house = "");
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, string title, string description);
        Task IncreaseQuantityAsync(Guid id, int quantity);
        Task DecreaseQuantityAsync(Guid id, int quantity);
        Task LendAsync(Guid bookId, Guid userId);
        Task ReturnAsync(Guid bookId, Guid userId);
    }
}
