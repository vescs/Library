using FluentAssertions;
using Library.Api;
using Library.Infrastructure.Commands.Books;
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
    public class BooksControllerTests : ControllerTestsBase
    {
        public BooksControllerTests(WebApplicationFactory<Startup> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData("books")]
        public async Task Get_books_endpoint_should_return_not_empty_collection(string url)
        {
            var client = _factory.CreateClient();

            var books = await GetDeserializedBooksAsync(client, url);

            books.Should().NotBeNull();
            books.Should().HaveCount(x => x > 0);
        }

        [Theory]
        [InlineData("books")]
        public async Task It_shouldnt_be_possible_to_add_book_for_unauthorized_user(string url)
        {
            var client = _factory.CreateClient();

            CreateBook command = new CreateBook
            {
                Id = Guid.NewGuid()
            };
            var stringContent = CreateStringContent(command);

            var response = await client.PostAsync(url, stringContent);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Theory]
        [InlineData("books")]
        public async Task It_should_be_possible_to_fetch_book_per_its_id(string url)
        {
            var client = _factory.CreateClient();

            var books = await GetDeserializedBooksAsync(client, url);
            var book = books.FirstOrDefault();

            var expectedbook = await GetDeserializedBookAsync(client, $"{url}/{book.Id}");

            expectedbook.Id.Should().Be(book.Id);
        }

        [Theory]
        [InlineData("books")]
        public async Task Incorrect_id_should_return_404(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"{url}/{Guid.NewGuid()}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private static async Task<IEnumerable<BookDTO>> GetDeserializedBooksAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<BookDTO>>(content);
        }

        private static async Task<BookDetailsDTO> GetDeserializedBookAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BookDetailsDTO>(content);
        }
    }
}
