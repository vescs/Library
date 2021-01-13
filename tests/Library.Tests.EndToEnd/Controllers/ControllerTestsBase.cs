using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Library.Api;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Library.Infrastructure.Settings;
using System.IO;

namespace Library.Tests.EndToEnd.Controllers
{
    public class ControllerTestsBase : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly WebApplicationFactory<Startup> _factory;

        public ControllerTestsBase(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        protected StringContent CreateStringContent<T>(T command) where T : class
        {
            var @object = JsonConvert.SerializeObject(command);
            return new StringContent(@object, UnicodeEncoding.UTF8, "application/json");
        }

   
    }
}
