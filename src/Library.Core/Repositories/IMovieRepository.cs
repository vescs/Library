using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Repositories
{
    public interface IMovieRepository
    {
        Task AddAsync(Movie movie);
        Task<IEnumerable<Movie>> BrowseAsync(string name = "");
        Task DeleteAsync(Guid id);
        Task<Movie> GetAsync(Guid id);
        Task<Newspaper> GetAsync(string title);
        Task UpdateAsync(Movie movie);
    }
}
