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
    public class UserRepositoryTests
    {
        protected List<User> userList = new List<User>()
        {
            new User(Guid.NewGuid(), "testusername", "testemail@gmail.com", "testpassword", "testsalt", "user", "testname", "testname"),
        };

        [Fact]
        public async Task Add_async_should_properly_add_user()
        {
            var userRepository = new InMemoryUserRepository();
            var user = userList.FirstOrDefault();

            await userRepository.AddAsync(user);
            var fetchedUser = await userRepository.GetAsync(user.Id);
            await userRepository.DeleteAsync(user.Id);

            fetchedUser.Should().NotBeNull();
            fetchedUser.Id.Should().Be(user.Id);
            fetchedUser.Should().BeEquivalentTo(user);
        }
    }
}
