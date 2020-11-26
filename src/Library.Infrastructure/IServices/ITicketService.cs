using Library.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.IServices
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDTO>> GetForUserAsync(Guid userId);
        Task<TicketDetailsDTO> GetAsync(Guid userId, Guid eventId, Guid ticketId);
        Task PurchaseAsync(Guid userId, Guid eventId, int amount, bool seat);
        Task CancelAsync(Guid userId, Guid eventId, int amount, bool seat);
    }
}
