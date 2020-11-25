using Library.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.IServices
{
    public interface IMovieService
    {
        Task CreateAsync(Guid id, string title, string description, string director, int length, int quantity, DateTime premiereDate);
        Task<IEnumerable<MovieDTO>> BrowseAsync(string title = "");
        Task<MovieDetailsDTO> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, string description);
        Task IncreaseQuantityAsync(Guid id, int quantity);
        Task DecreaseQuantityAsync(Guid id, int quantity);
        Task LendAsync(Guid movieId, Guid userId);
        Task ReturnAsync(Guid movieId, Guid userId);
    }
}
