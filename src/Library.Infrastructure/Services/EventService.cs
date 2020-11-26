using AutoMapper;
using Library.Core.Models;
using Library.Core.Repositories;
using Library.Infrastructure.DTO;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task AddTicketsAsync(Guid eventId, int amount, decimal price, bool seat)
        {
            var @event = await _eventRepository.GetAsync(eventId);
            if (@event == null)
            {
                throw new Exception($"Event with id {eventId} does not exist.");
            }
            @event.AddTickets(amount, price, seat);
            await _eventRepository.UpdateAsync(@event);
        }

        public async Task<IEnumerable<EventDTO>> BrowseAsync(string title = "")
        {
            var events = await _eventRepository.BrowseAsync(title);
            return _mapper.Map<IEnumerable<EventDTO>>(events);
        }

        public async Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            var @event = await _eventRepository.GetAsync(id);
            if (@event != null)
            {
                throw new Exception($"Event with id {id} already exists.");
            }
            @event = await _eventRepository.GetAsync(name);
            if (@event != null)
            {
                throw new Exception($"Event with name '{name}' already exists.");
            }
            @event = new Event(id, name, description, startDate, endDate);
            await _eventRepository.AddAsync(@event);
        }

        public async Task DeleteAsync(Guid id)
        {
            var @event = await _eventRepository.GetAsync(id);
            if (@event == null)
            {
                throw new Exception($"Event with id {id} does not exist.");
            }
            await _eventRepository.DeleteAsync(id);
        }

        public async Task<EventDetailsDTO> GetAsync(Guid id)
        {
            var @event = await _eventRepository.GetAsync(id);
            if (@event == null)
            {
                throw new Exception($"Event with id {id} does not exist.");
            }
            return _mapper.Map<EventDetailsDTO>(@event);
        }

        public async Task<EventDetailsDTO> GetAsync(string name)
        {
            var @event = await _eventRepository.GetAsync(name);
            if (@event == null)
            {
                throw new Exception($"Event with name '{name}' does not exist.");
            }
            return _mapper.Map<EventDetailsDTO>(@event);
        }

        public async Task UpdateAsync(Guid id, string description)
        {
            var @event = await _eventRepository.GetAsync(id);
            if (@event == null)
            {
                throw new Exception($"Event with id {id} does not exist.");
            }
            @event.SetDescription(description);
            await _eventRepository.UpdateAsync(@event);
        }
    }
}
