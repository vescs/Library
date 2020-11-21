using Library.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.IServices
{
    public interface INewspaperService
    {
        Task CreateAsync(Guid id, string title, string description, string type, DateTime releaseDate);
        Task<NewspaperDTO> GetAsync(Guid id);
        Task<IEnumerable<NewspaperDTO>> BrowseAsync(string title = "");
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, string description);
    }
}
