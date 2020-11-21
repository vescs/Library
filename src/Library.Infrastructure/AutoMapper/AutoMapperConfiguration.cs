using AutoMapper;
using Library.Core.Models;
using Library.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookDTO>();
                cfg.CreateMap<Newspaper, NewspaperDTO>();
            })
            .CreateMapper();
    }
}
