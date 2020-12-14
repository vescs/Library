using FluentAssertions;
using Library.Core.Models;
using Library.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests.Repositories.InMemory
{
    public class MovieRepositoryTests
    {
        protected List<Movie> movieList = new List<Movie>()
        {
            new Movie(Guid.NewGuid(), "TestTitle1", "TestDescription", "TestDirector", 2000, 20, DateTime.UtcNow),
            new Movie(Guid.NewGuid(), "TestTitle2", "TestDescription", "TestDirector", 2000, 20, DateTime.UtcNow),
            new Movie(Guid.NewGuid(), "TestTitle3", "TestDescription", "TestDirector", 2000, 20, DateTime.UtcNow),
            new Movie(Guid.NewGuid(), "TestTitle4", "TestDescription", "TestDirector", 2000, 20, DateTime.UtcNow),
            new Movie(Guid.NewGuid(), "TestTitle5", "TestDescription", "TestDirector", 2000, 20, DateTime.UtcNow),
        };

        [Fact]
        public async Task Add_async_should_properly_add_movie_so_it_can_be_fetched()
        {
            var movieRepository = new InMemoryMovieRepository();
            var movie = movieList.FirstOrDefault();

            await movieRepository.AddAsync(movie);
            var fetchedNewspaper = await movieRepository.GetAsync(movie.Id);
            await movieRepository.DeleteAsync(movie.Id);

            fetchedNewspaper.Should().NotBeNull();
            fetchedNewspaper.Should().BeEquivalentTo(movie);
        }

        [Fact]
        public async Task Browse_async_should_return_collection_of_movies()
        {
            var movieRepository = new InMemoryMovieRepository();

            foreach (var m in movieList)
            {
                await movieRepository.AddAsync(m);
            }
            var fetchedMovies = await movieRepository.BrowseAsync();

            fetchedMovies.Should().NotBeEmpty();
            fetchedMovies.Should().HaveCount(movieList.Count);
            fetchedMovies.Should().BeEquivalentTo(movieList);
        }

        [Fact]
        public async Task Delete_async_should_properly_delete_movie_so_it_cant_get_fetched()
        {
            var movieRepository = new InMemoryMovieRepository();
            var movie = movieList.FirstOrDefault();

            await movieRepository.DeleteAsync(movie.Id);
            var fetchedMovie = await movieRepository.GetAsync(movie.Id);

            fetchedMovie.Should().BeNull();
            fetchedMovie.Should().NotBeEquivalentTo(movie);
        }
    }
}
