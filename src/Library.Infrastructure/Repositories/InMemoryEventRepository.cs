﻿using Library.Core.Models;
using Library.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class InMemoryEventRepository : IEventRepository
    {
        private static ISet<Event> _events = new HashSet<Event>()
        {
            new Event(Guid.NewGuid(), "Name 1", "Description 1", DateTime.UtcNow, DateTime.UtcNow.AddDays(2)),
            new Event(Guid.NewGuid(), "Name 2", "Description 2", DateTime.UtcNow, DateTime.UtcNow.AddDays(3)),
            new Event(Guid.NewGuid(), "Name 3", "Description 3", DateTime.UtcNow, DateTime.UtcNow.AddDays(4)),
        };
        public async Task AddAsync(Event @event)
        {
            _events.Add(@event);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Event>> BrowseAsync(string name = "")
        {
            var events = _events.Where(x => x.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));
            return await Task.FromResult(events);
        }

        public async Task DeleteAsync(Guid id)
        {
            var @event = _events.SingleOrDefault(x => x.Id == id);
            if(@event == null)
            {
                return;
            }
            _events.Remove(@event);
            await Task.CompletedTask;
        }

        public async Task<Event> GetAsync(Guid id)
        {
            var @event = _events.SingleOrDefault(x => x.Id == id);
            return await Task.FromResult(@event);
        }

        public async Task<Event> GetAsync(string name)
        {
            var @event = _events.SingleOrDefault(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant());
            return await Task.FromResult(@event);
        }

        public async Task UpdateAsync(Event @event)
        {
            await Task.CompletedTask;
        }
    }
}
