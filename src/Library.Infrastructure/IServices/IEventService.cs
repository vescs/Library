using Library.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.IServices
{
    public interface IEventService
    {
        Task AddTicketsAsync(Guid eventId, int amount, decimal price, bool seat);
        Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate);
        Task<IEnumerable<EventDTO>> BrowseAsync(string name = "");
        Task<EventDetailsDTO> GetAsync(Guid id);
        Task<EventDetailsDTO> GetAsync(string name);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, string description);
    }
}
