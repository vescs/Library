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
    public class EventRepositoryTests
    {
        protected List<Event> movieList = new List<Event>()
        {
            new Event(Guid.NewGuid(), "TestTitle1", "TestDescription", DateTime.UtcNow, DateTime.UtcNow.AddDays(1)),
            new Event(Guid.NewGuid(), "TestTitle2", "TestDescription", DateTime.UtcNow, DateTime.UtcNow.AddDays(1)),
            new Event(Guid.NewGuid(), "TestTitle3", "TestDescription", DateTime.UtcNow, DateTime.UtcNow.AddDays(1)),
            new Event(Guid.NewGuid(), "TestTitle4", "TestDescription", DateTime.UtcNow, DateTime.UtcNow.AddDays(1)),
            new Event(Guid.NewGuid(), "TestTitle5", "TestDescription", DateTime.UtcNow, DateTime.UtcNow.AddDays(1)),
        };
        protected InMemoryEventRepository eventRepository = new InMemoryEventRepository();

        [Fact]
        public async Task Add_async_should_properly_add_event_so_it_can_be_fetched()
        {
            var eventRepository = new InMemoryEventRepository();
            var @event = movieList.Skip(1).FirstOrDefault();

            await eventRepository.AddAsync(@event);
            var fetchedEvent = await eventRepository.GetAsync(@event.Id);
            await eventRepository.DeleteAsync(@event.Id);

            fetchedEvent.Should().NotBeNull();
            fetchedEvent.Should().BeEquivalentTo(@event);
        }

        [Fact]
        public async Task Browse_async_should_return_collection_of_events()
        {
            var eventRepository = new InMemoryEventRepository();

            foreach (var e in movieList)
            {
                await eventRepository.AddAsync(e);
            }
            var fetchedEvents = await eventRepository.BrowseAsync();

            fetchedEvents.Should().NotBeEmpty();
            fetchedEvents.Should().HaveCount(movieList.Count);
            fetchedEvents.Should().BeEquivalentTo(movieList);
        }

        [Fact]
        public async Task Delete_async_should_properly_delete_event_so_it_cant_get_fetched()
        {
            var eventRepository = new InMemoryEventRepository();
            var @event = movieList.FirstOrDefault();

            await eventRepository.DeleteAsync(@event.Id);
            var fetchedEvent = await eventRepository.GetAsync(@event.Id);

            fetchedEvent.Should().BeNull();
            fetchedEvent.Should().NotBeEquivalentTo(@event);
        }

        
    }
}
