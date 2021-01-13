using FluentAssertions;
using Library.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests.EndToEnd.Controllers
{
    public class UsersControllerTests : ControllerTestsBase
    {
        public UsersControllerTests(WebApplicationFactory<Startup> factory) : base(factory)
        {

        }

        [Fact]
        public async Task unauthorized_user_should_get_401_from_get_account_endpoint()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("users");
            
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
