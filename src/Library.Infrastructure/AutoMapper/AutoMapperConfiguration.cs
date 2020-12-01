using AutoMapper;
using Library.Core.Models;
using Library.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookDTO>();
                cfg.CreateMap<Book, BookDetailsDTO>();
                cfg.CreateMap<Newspaper, NewspaperDTO>();
                cfg.CreateMap<Newspaper, NewspaperDetailsDTO>();
                cfg.CreateMap<Event, EventDTO>();
                cfg.CreateMap<Event, EventDetailsDTO>()
                    .ForMember(x => x.AvailableTickets, y => y.MapFrom(z => z.AvailableTickets.Count()))
                    .ForMember(x => x.PurchasedTickets, y => y.MapFrom(z => z.PurchasedTickets.Count()))
                    .ForMember(x => x.AvailableSeats, y => y.MapFrom(z => z.AvailableTickets.Where(t => t.Seat == true).Count()));
                cfg.CreateMap<Movie, MovieDTO>();
                cfg.CreateMap<Movie, MovieDetailsDTO>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Ticket, TicketDTO>();
                cfg.CreateMap<Ticket, TicketDetailsDTO>();
            })
            .CreateMapper();
    }
}
