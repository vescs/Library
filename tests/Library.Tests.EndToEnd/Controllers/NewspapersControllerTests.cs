using FluentAssertions;
using Library.Api;
using Library.Infrastructure.Commands.Newspapers;
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
    public class NewspapersControllerTests : ControllerTestsBase
    {
        public NewspapersControllerTests(WebApplicationFactory<Startup> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData("newspapers")]
        public async Task Get_newspapers_endpoint_should_return_not_empty_collection(string url)
        {
            var client = _factory.CreateClient();

            var newspapers = await GetDeserializedNewspapersAsync(client, url);

            newspapers.Should().NotBeNull();
            newspapers.Should().HaveCount(x => x > 0);
        }

        [Theory]
        [InlineData("newspapers")]
        public async Task It_shouldnt_be_possible_to_add_newspaper_for_unauthorized_user(string url)
        {
            var client = _factory.CreateClient();

            CreateNewspaper command = new CreateNewspaper
            {
                Id = Guid.NewGuid()
            };
            var stringContent = CreateStringContent(command);

            var response = await client.PostAsync(url, stringContent);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Theory]
        [InlineData("newspapers")]
        public async Task It_should_be_possible_to_fetch_newspaper_per_its_id(string url)
        {
            var client = _factory.CreateClient();

            var newspapers = await GetDeserializedNewspapersAsync(client, url);
            var newspaper = newspapers.FirstOrDefault();

            var expectedNewspaper = await GetDeserializedNewspaperAsync(client, $"{url}/{newspaper.Id}");

            expectedNewspaper.Id.Should().Be(newspaper.Id);
        }

        [Theory]
        [InlineData("newspapers")]
        public async Task Incorrect_id_should_return_404(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"{url}/{Guid.NewGuid()}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private static async Task<IEnumerable<NewspaperDTO>> GetDeserializedNewspapersAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<NewspaperDTO>>(content);
        }

        private static async Task<NewspaperDetailsDTO> GetDeserializedNewspaperAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<NewspaperDetailsDTO>(content);
        }
    }
}
