using FluentAssertions;
using Library.Api;
using Library.Infrastructure.Commands.Events;
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
    public class EventsControllerTests : ControllerTestsBase
    {
        public EventsControllerTests(WebApplicationFactory<Startup> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData("events")]
        public async Task Get_events_endpoint_should_return_not_empty_collection(string url)
        {
            var client = _factory.CreateClient();

            var events = await GetDeserializedEventsAsync(client, url);

            events.Should().NotBeNull();
            events.Should().HaveCount(x => x > 0);
        }

        [Theory]
        [InlineData("events")]
        public async Task It_shouldnt_be_possible_to_add_event_for_unauthorized_user(string url)
        {
            var client = _factory.CreateClient();

            CreateEvent command = new CreateEvent
            {
                Id = Guid.NewGuid(),
            };
            var stringContent = CreateStringContent(command);

            var response = await client.PostAsync(url, stringContent);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Theory]
        [InlineData("events")]
        public async Task It_should_be_possible_to_fetch_event_per_its_id(string url)
        {
            var client = _factory.CreateClient();

            var events = await GetDeserializedEventsAsync(client, url);
            var @event = events.FirstOrDefault();

            var expectedEvent = await GetDeserializedEventAsync(client, $"{url}/{@event.Id}");

            expectedEvent.Id.Should().Be(@event.Id);
        }

        [Theory]
        [InlineData("events")]
        public async Task Incorrect_id_should_return_404(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"{url}/{Guid.NewGuid()}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private static async Task<IEnumerable<EventDTO>> GetDeserializedEventsAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<EventDTO>>(content);
        }

        private static async Task<EventDetailsDTO> GetDeserializedEventAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EventDetailsDTO>(content);
        }


    }
}
