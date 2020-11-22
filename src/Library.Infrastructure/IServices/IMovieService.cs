using Library.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.IServices
{
    public interface IMovieService
    {
        Task CreateAsync(Guid id, string title, string description, string director, int length, DateTime premiereDate);
        Task<IEnumerable<MovieDTO>> BrowseAsync(string title = "");
        Task<MovieDTO> GetAsync(Guid id);
        Task<MovieDTO> GetAsync(string title);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, string description);
    }
}
