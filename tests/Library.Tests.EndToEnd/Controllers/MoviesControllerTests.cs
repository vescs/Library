using FluentAssertions;
using Library.Api;
using Library.Infrastructure.Commands.Movies;
using Library.Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests.EndToEnd.Controllers
{
    public class MoviesControllerTests : ControllerTestsBase
    {
        public MoviesControllerTests(WebApplicationFactory<Startup> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData("movies")]
        public async Task Get_movies_endpoint_should_return_not_empty_collection(string url)
        {
            var client = _factory.CreateClient();

            var movies = await GetDeserializedMoviesAsync(client, url);

            movies.Should().NotBeNull();
            movies.Should().HaveCount(x => x > 0);
        }

        [Theory]
        [InlineData("movies")]
        public async Task It_shouldnt_be_possible_to_add_movie_for_unauthorized_user(string url)
        {
            var client = _factory.CreateClient();

            CreateMovie command = new CreateMovie
            {
                Id = Guid.NewGuid(),
            };
            var stringContent = CreateStringContent(command);

            var response = await client.PostAsync(url, stringContent);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Theory]
        [InlineData("movies")]
        public async Task It_should_be_able_to_fetch_movie_per_its_id(string url)
        {
            var client = _factory.CreateClient();

            var movies = await GetDeserializedMoviesAsync(client, url);
            var movie = movies.FirstOrDefault();

            var expectedMovie = await GetDeserializedMovieAsync(client, $"{url}/{movie.Id}");
            
            expectedMovie.Id.Should().Be(movie.Id);
        }

        [Theory]
        [InlineData("movies")]
        public async Task Incorrect_id_should_return_404(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"{url}/{Guid.NewGuid()}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private static async Task<IEnumerable<MovieDTO>> GetDeserializedMoviesAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<MovieDTO>>(content);
        }

        private static async Task<MovieDetailsDTO> GetDeserializedMovieAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MovieDetailsDTO>(content);
        }
    }
}
