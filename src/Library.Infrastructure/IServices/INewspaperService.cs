using Library.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.IServices
{
    public interface INewspaperService
    {
        Task CreateAsync(Guid id, string title, string description, string type, int quantity, DateTime releaseDate);
        Task<NewspaperDetailsDTO> GetAsync(Guid id);
        Task<IEnumerable<NewspaperDTO>> BrowseAsync(string title = "");
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, string description);
        Task IncreaseQuantityAsync(Guid id, int quantity);
        Task DecreaseQuantityAsync(Guid id, int quantity);
        Task LendAsync(Guid newspaperId, Guid userId);
        Task ReturnAsync(Guid newspaperId, Guid userId);
    }
}
