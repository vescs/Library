
using Library.Core.Models;
using Library.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class InMemoryMovieRepository : IMovieRepository
    {
        private static ISet<Movie> _movies = new HashSet<Movie>();
        public async Task AddAsync(Movie movie)
        {
            _movies.Add(movie);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Movie>> BrowseAsync(string name = "")
        {
            var movies = _movies.Where(x => x.Title.ToLowerInvariant().Contains(name.ToLowerInvariant()));
            return await Task.FromResult(movies);
        }

        public async Task DeleteAsync(Guid id)
        {
            var movie = _movies.SingleOrDefault(x => x.Id == id);
            if(movie == null)
            {
                return;
            }
            _movies.Remove(movie);
            await Task.CompletedTask;
        }

        public async Task<Movie> GetAsync(Guid id)
        {
            var movie = _movies.SingleOrDefault(x => x.Id == id);
            return await Task.FromResult(movie);
        }

        public async Task<Movie> GetAsync(string title)
        {
            var movie = _movies.SingleOrDefault(x => x.Title.ToLowerInvariant() == title.ToLowerInvariant());
            return await Task.FromResult(movie);
        }

        public async Task UpdateAsync(Movie movie)
        {
            await Task.CompletedTask;
        }
    }
}
