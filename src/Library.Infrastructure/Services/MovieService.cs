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
                throw new Exception($"Movie with id {id} already exists.");
            }
            movie = new Movie(id, title, description, director, length, quantity, premiereDate);
            await _movieRepository.AddAsync(movie);
        }

        public async Task DecreaseQuantityAsync(Guid id, int quantity)
        {
            var movie = await _movieRepository.GetAsync(id);
            if (movie == null)
            {
                throw new Exception($"Movie with id {id} does not exist.");
            }
            movie.DecreaseQuantity(quantity);
            await _movieRepository.UpdateAsync(movie);
        }

        public async Task DeleteAsync(Guid id)
        {
            var movie = await _movieRepository.GetAsync(id);
            if(movie == null)
            {
                throw new Exception($"Movie with id {id} does not exist.");
            }
            await _movieRepository.DeleteAsync(id);
        }

        public async Task<MovieDetailsDTO> GetAsync(Guid id)
        {
            var movie = await _movieRepository.GetAsync(id);
            if (movie == null)
            {
                throw new Exception($"Movie with id {id} does not exist.");
            }
            return _mapper.Map<MovieDetailsDTO>(movie);
        }


        public async Task IncreaseQuantityAsync(Guid id, int quantity)
        {
            var movie = await _movieRepository.GetAsync(id);
            if (movie == null)
            {
                throw new Exception($"Movie with id {id} does not exist.");
            }
            movie.IncreaseQuantity(quantity);
            await _movieRepository.UpdateAsync(movie);
        }

        public async Task LendAsync(Guid movieId, Guid userId)
        {
            var movie = await _movieRepository.GetAsync(movieId);
            if (movie == null)
            {
                throw new Exception($"Book with id {movieId} does not exist.");
            }
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with id {userId} does not exist.");
            }
            movie.Lend(user);
            await _movieRepository.UpdateAsync(movie);
        }

        public async Task ReturnAsync(Guid movieId, Guid userId)
        {
            var movie = await _movieRepository.GetAsync(movieId);
            if (movie == null)
            {
                throw new Exception($"Book with id {movieId} does not exist.");
            }
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with id {userId} does not exist.");
            }
            movie.Return(user);
            await _movieRepository.UpdateAsync(movie);
        }

        public async Task UpdateAsync(Guid id, string description)
        {
            var movie = await _movieRepository.GetAsync(id);
            if(movie == null)
            {
                throw new Exception($"Movie with id {id} does not exist.");
            }
            movie.SetDescription(description);
            await _movieRepository.UpdateAsync(movie);
        }
    }
}
