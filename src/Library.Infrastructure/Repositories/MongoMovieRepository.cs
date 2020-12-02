using Library.Core.Models;
using Library.Core.Repositories;
using Library.Infrastructure.Extentions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class MongoMovieRepository : IMovieRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<Movie> Movies => _database.GetCollection<Movie>("Movies");

        public MongoMovieRepository(IConfiguration configuration)
        {
            _database = configuration.GetMongoDatabase();
        }

        public async Task AddAsync(Movie movie)
            => await Movies.InsertOneAsync(movie);

        public async Task<IEnumerable<Movie>> BrowseAsync(string title = "")
        {
            var list = await Movies.AsQueryable().
                Where(x => x.Title.ToLowerInvariant()
                .Contains(title.ToLowerInvariant()))
                .ToListAsync();
            return list.OrderBy(x => x.Title).AsEnumerable();
        }

        public async Task DeleteAsync(Guid id)
            => await Movies.DeleteOneAsync(x => x.Id == id);

        public async Task<Movie> GetAsync(Guid id)
            => await Movies.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(Movie movie)
            => await Movies.ReplaceOneAsync(x => x.Id == movie.Id, movie);
    }
}
