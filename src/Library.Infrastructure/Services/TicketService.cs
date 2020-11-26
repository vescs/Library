using AutoMapper;
using Library.Core.Repositories;
using Library.Infrastructure.DTO;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public TicketService(IEventRepository eventRepository, IMapper mapper, IUserRepository userRepository)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task CancelAsync(Guid userId, Guid eventId, int amount, bool seat)
        {
            var user = await _userRepository.GetAsync(userId);
            if(user == null)
            {
                throw new Exception($"User with id {userId} does not exist.");
            }
            var @event = await _eventRepository.GetAsync(eventId);
            if (@event == null)
            {
                throw new Exception($"Event with id {eventId} does not exist.");
            }
            @event.CancelTickets(user, amount, seat);
            await _eventRepository.UpdateAsync(@event);
        }

        public async Task<TicketDetailsDTO> GetAsync(Guid userId, Guid eventId, Guid ticketId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with id {userId} does not exist.");
            }
            var @event = await _eventRepository.GetAsync(eventId);
            if (@event == null)
            {
                throw new Exception($"Event with id {eventId} does not exist.");
            }
            var ticket = @event.Tickets.SingleOrDefault(x => x.Id == ticketId);
            return _mapper.Map<TicketDetailsDTO>(ticket);
        }

        public async Task<IEnumerable<TicketDTO>> GetForUserAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with id {userId} does not exist.");
            }
            var events = await _eventRepository.BrowseAsync();
            var tickets = events.SelectMany(x => x.TicketsBoughtByUser(user));
            return _mapper.Map<IEnumerable<TicketDTO>>(tickets);
        }

        public async Task PurchaseAsync(Guid userId, Guid eventId, int amount, bool seat)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with id {userId} does not exist.");
            }
            var @event = await _eventRepository.GetAsync(eventId);
            if (@event == null)
            {
                throw new Exception($"Event with id {eventId} does not exist.");
            }
            @event.PurchaseTickets(user, amount, seat);
            await _eventRepository.UpdateAsync(@event);
        }
    }
}
