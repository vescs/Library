using Library.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.IServices
{
    public interface IEventService
    {
        Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate);
        Task<IEnumerable<EventDTO>> BrowseAsync(string name = "");
        Task<EventDTO> GetAsync(Guid id);
        Task<EventDTO> GetAsync(string title);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, string description);
    }
}
