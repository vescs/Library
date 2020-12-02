﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Mongo
{
    public class MongoConfiguration
    {
        private static bool _initialized;
        public static void Initialize()
        {
            if (_initialized)
            {
                return;
            }
            _initialized = true;
            RegisterConventions();
        }
        private static void RegisterConventions()
        {
            ConventionRegistry.Register("LibraryConventions", new MongoConvention(), x => true);
        }
        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions
                => new List<IConvention>
                {
                    new IgnoreExtraElementsConvention(true),
                    new CamelCaseElementNameConvention(),
                    new EnumRepresentationConvention(BsonType.String)
                };
        }
    }
}
