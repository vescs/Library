using Library.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Extentions
{
    public static class MongoExtentions
    {
        public static IMongoDatabase GetMongoDatabase(this IConfiguration configuration)
        {
            var mongoSettings = new MongoSettings();
            configuration.GetSection("mongo").Bind(mongoSettings);
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            return mongoClient.GetDatabase(mongoSettings.Database);
        }
    }
}
