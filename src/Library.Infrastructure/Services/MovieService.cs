using AutoMapper;
using Library.Core.Models;
using Library.Core.Repositories;
using Library.Infrastructure.DTO;
using Library.Infrastructure.Exceptions;
using Library.Infrastructure.Extentions;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository, IMapper mapper, IUserRepository userRepository)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<MovieDTO>> BrowseAsync(string name = "")
        {
            var movies = await _movieRepository.BrowseAsync(name);
            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }

        public async Task CreateAsync(Guid id, string title, string description, string director, 
            int length, int quantity, DateTime premiereDate)
        {
            var movie = await _movieRepository.GetAsync(id);
            if(movie != null)
            {
                throw new ServiceException(ServiceErrorCodes.AlreadyExists, 
                    $"Movie with id {id} already exists.");
            }
            movie = new Movie(id, title, description, director, length, quantity, premiereDate);
            await _movieRepository.AddAsync(movie);
        }

        public async Task DecreaseQuantityAsync(Guid id, int quantity)
        {
            var movie = await _movieRepository.SafeGetAsync(id);
            movie.DecreaseQuantity(quantity);
            await _movieRepository.UpdateAsync(movie);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _movieRepository.SafeGetAsync(id);
            await _movieRepository.DeleteAsync(id);
        }

        public async Task<MovieDetailsDTO> GetAsync(Guid id)
        {
            var movie = await _movieRepository.SafeGetAsync(id);
            return _mapper.Map<MovieDetailsDTO>(movie);
        }


        public async Task IncreaseQuantityAsync(Guid id, int quantity)
        {
            var movie = await _movieRepository.SafeGetAsync(id);
            movie.IncreaseQuantity(quantity);
            await _movieRepository.UpdateAsync(movie);
        }

        public async Task LendAsync(Guid movieId, Guid userId)
        {
            var movie = await _movieRepository.SafeGetAsync(movieId);
            var user = await _userRepository.SafeGetAsync(userId);
            movie.Lend(user);
            await _movieRepository.UpdateAsync(movie);
        }

        public async Task ReturnAsync(Guid movieId, Guid userId)
        {
            var movie = await _movieRepository.SafeGetAsync(movieId);
            var user = await _userRepository.SafeGetAsync(userId);
            movie.Return(user);
            await _movieRepository.UpdateAsync(movie);
        }

        public async Task UpdateAsync(Guid id, string description)
        {
            var movie = await _movieRepository.SafeGetAsync(id);
            movie.SetDescription(description);
            await _movieRepository.UpdateAsync(movie);
        }
    }
}
