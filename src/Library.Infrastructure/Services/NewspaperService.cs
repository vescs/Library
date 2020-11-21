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
    public class NewspaperService : INewspaperService
    {
        private readonly INewspaperRepository _newspaperRepository;
        private readonly IMapper _mapper;
        public NewspaperService(IMapper mapper, INewspaperRepository newspaperRepository)
        {
            _newspaperRepository = newspaperRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<NewspaperDTO>> BrowseAsync(string title = "")
        {
            var newspapers = await _newspaperRepository.BrowseAsync(title);
            return _mapper.Map<IEnumerable<NewspaperDTO>>(newspapers);
        }

        public async Task CreateAsync(Guid id, string title, string description, string type, DateTime releaseDate)
        {
            var newspaper = await _newspaperRepository.GetAsync(id);
            if(newspaper != null)
            {
                throw new Exception($"Newspaper with id {id} already exists.");
            }
            newspaper = new Newspaper(id, title, description, type, releaseDate);
            await _newspaperRepository.AddAsync(newspaper);
        }

        public async Task DeleteAsync(Guid id)
        {
            var newspaper = await _newspaperRepository.GetAsync(id);
            if (newspaper == null)
            {
                throw new Exception($"Newspaper with id {id} does not exist.");
            }
            await _newspaperRepository.DeleteAsync(id);
        }

        public async Task<NewspaperDTO> GetAsync(Guid id)
        {
            var newspaper = await _newspaperRepository.GetAsync(id);
            if(newspaper == null)
            {
                throw new Exception($"Newspaper with id {id} does not exist.");
            }
            return _mapper.Map<NewspaperDTO>(newspaper);
        }

        public async Task UpdateAsync(Guid id, string description)
        {
            var newspaper = await _newspaperRepository.GetAsync(id);
            if(newspaper == null)
            {
                throw new Exception($"Newspaper with id {id} does not exist.");
            }
            newspaper.SetDescription(description);
            await _newspaperRepository.UpdateAsync(newspaper);
        }
    }
}
