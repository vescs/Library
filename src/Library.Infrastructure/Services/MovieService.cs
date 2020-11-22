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
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MovieDTO>> BrowseAsync(string name = "")
        {
            var movies = await _movieRepository.BrowseAsync(name);
            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }

        public async Task CreateAsync(Guid id, string title, string description, string director, int length, DateTime premiereDate)
        {
            var movie = await _movieRepository.GetAsync(id);
            if(movie != null)
            {
                throw new Exception($"Movie with id {id} already exists.");
            }
            movie = await _movieRepository.GetAsync(title);
            if (movie != null)
            {
                throw new Exception($"Movie with title '{title}' already exists.");
            }
            movie = new Movie(id, title, description, director, length, premiereDate);
            await _movieRepository.AddAsync(movie);
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

        public async Task<MovieDTO> GetAsync(Guid id)
        {
            var movie = await _movieRepository.GetAsync(id);
            if (movie == null)
            {
                throw new Exception($"Movie with id {id} does not exist.");
            }
            return _mapper.Map<MovieDTO>(movie);
        }

        public async Task<MovieDTO> GetAsync(string title)
        {
            var movie = await _movieRepository.GetAsync(title);
            if (movie == null)
            {
                throw new Exception($"Movie with title '{title}' does not exist.");
            }
            return _mapper.Map<MovieDTO>(movie);
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
