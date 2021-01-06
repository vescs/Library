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
    public class NewspaperService : INewspaperService
    {
        private readonly IUserRepository _userRepository;
        private readonly INewspaperRepository _newspaperRepository;
        private readonly IMapper _mapper;
        public NewspaperService(IMapper mapper, INewspaperRepository newspaperRepository, IUserRepository userRepository)
        {
            _newspaperRepository = newspaperRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<NewspaperDTO>> BrowseAsync(string title = "")
        {
            var newspapers = await _newspaperRepository.BrowseAsync(title);
            return _mapper.Map<IEnumerable<NewspaperDTO>>(newspapers);
        }

        public async Task CreateAsync(Guid id, string title, string description, string type, int quantity, DateTime releaseDate)
        {
            var newspaper = await _newspaperRepository.GetAsync(id);
            if(newspaper != null)
            {
                throw new ServiceException(ServiceErrorCodes.AlreadyExists, 
                    $"Newspaper with id {id} already exists.");
            }
            newspaper = new Newspaper(id, title, description, type, quantity, releaseDate);
            await _newspaperRepository.AddAsync(newspaper);
        }

        public async Task DecreaseQuantityAsync(Guid id, int quantity)
        {
            var newspaper = await _newspaperRepository.SafeGetAsync(id);
            newspaper.DecreaseQuantity(quantity);
            await _newspaperRepository.UpdateAsync(newspaper);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _newspaperRepository.SafeGetAsync(id);
            await _newspaperRepository.DeleteAsync(id);
        }

        public async Task<NewspaperDetailsDTO> GetAsync(Guid id)
        {
            var newspaper = await _newspaperRepository.SafeGetAsync(id);
            return _mapper.Map<NewspaperDetailsDTO>(newspaper);
        }

        public async Task IncreaseQuantityAsync(Guid id, int quantity)
        {
            var newspaper = await _newspaperRepository.SafeGetAsync(id);
            newspaper.IncreaseQuantity(quantity);
            await _newspaperRepository.UpdateAsync(newspaper);
        }

        public async Task LendAsync(Guid newspaperId, Guid userId)
        {
            var newspaper = await _newspaperRepository.SafeGetAsync(newspaperId);
            var user = await _userRepository.SafeGetAsync(userId);
            newspaper.Lend(user);
            await _newspaperRepository.UpdateAsync(newspaper);
        }

        public async Task ReturnAsync(Guid newspaperId, Guid userId)
        {
            var newspaper = await _newspaperRepository.SafeGetAsync(newspaperId);
            var user = await _userRepository.SafeGetAsync(userId);
            newspaper.Return(user);
            await _newspaperRepository.UpdateAsync(newspaper);
        }

        public async Task UpdateAsync(Guid id, string description)
        {
            var newspaper = await _newspaperRepository.SafeGetAsync(id);
            newspaper.SetDescription(description);
            await _newspaperRepository.UpdateAsync(newspaper);
        }
    }
}
