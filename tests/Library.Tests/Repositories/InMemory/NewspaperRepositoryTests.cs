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
    public class NewspaperRepositoryTests
    {
        protected List<Newspaper> newspaperList = new List<Newspaper>()
        {
            new Newspaper(Guid.NewGuid(), "TestTitle1", "TestDescription", "TestType", 2000, DateTime.UtcNow),
            new Newspaper(Guid.NewGuid(), "TestTitle2", "TestDescription", "TestType", 2000, DateTime.UtcNow),
            new Newspaper(Guid.NewGuid(), "TestTitle3", "TestDescription", "TestType", 2000, DateTime.UtcNow),
            new Newspaper(Guid.NewGuid(), "TestTitle4", "TestDescription", "TestType", 2000, DateTime.UtcNow),
            new Newspaper(Guid.NewGuid(), "TestTitle5", "TestDescription", "TestType", 2000, DateTime.UtcNow),
        };

        [Fact]
        public async Task Add_async_should_properly_add_newspaper_so_it_can_be_fetched()
        {
            var newspaperRepository = new InMemoryNewspaperRepository();
            var newspaper = newspaperList.FirstOrDefault();

            await newspaperRepository.AddAsync(newspaper);
            var fetchedNewspaper = await newspaperRepository.GetAsync(newspaper.Id);
            await newspaperRepository.DeleteAsync(newspaper.Id);

            fetchedNewspaper.Should().NotBeNull();
            fetchedNewspaper.Should().BeEquivalentTo(newspaper);
        }

        [Fact]
        public async Task Browse_async_should_return_collection_of_newspapers()
        {
            var newspaperRepository = new InMemoryNewspaperRepository();

            foreach (var n in newspaperList)
            {
                await newspaperRepository.AddAsync(n);
            }
            var fetchedNewspapers = await newspaperRepository.BrowseAsync();

            fetchedNewspapers.Should().NotBeEmpty();
            fetchedNewspapers.Should().HaveCount(newspaperList.Count);
            fetchedNewspapers.Should().BeEquivalentTo(newspaperList);
        }

        [Fact]
        public async Task Delete_async_should_properly_delete_newspaper_so_it_cant_get_fetched()
        {
            var newspaperRepository = new InMemoryNewspaperRepository();
            var newspaper = newspaperList.FirstOrDefault();

            await newspaperRepository.DeleteAsync(newspaper.Id);
            var fetchedNewspaper = await newspaperRepository.GetAsync(newspaper.Id);

            fetchedNewspaper.Should().BeNull();
            fetchedNewspaper.Should().NotBeEquivalentTo(newspaper);
        }
    }
}
