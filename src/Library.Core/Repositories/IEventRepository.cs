using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Repositories
{
    public interface IEventRepository
    {
        Task AddAsync(Event @event);
        Task<IEnumerable<Event>> BrowseAsync(string name = "");
        Task DeleteAsync(Guid id);
        Task<Event> GetAsync(Guid id);
        Task<Newspaper> GetAsync(string name);
        Task UpdateAsync(Event @event);
    }
}
