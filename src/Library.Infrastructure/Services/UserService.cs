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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;
        
        public UserService(IUserRepository userRepository, IMapper mapper, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _encrypter = encrypter;
        }

        public async Task<UserDTO> GetUser(string email)
        {
            var user = await _userRepository.GetAsync(email);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserInfoAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.SafeGetAsync(email);
            var hash = _encrypter.GetHash(password, user.Salt);
            if (user.Password != hash)
            {
                throw new ServiceException(ServiceErrorCodes.InvalidCredentials, 
                    "Invalid credentials");
            }
        }

        public async Task RegisterAsync(Guid id, string email, string username, string password, 
            string firstName, string lastName, string role = "user")
        {
            var user = await _userRepository.GetAsync(id);
            if (user != null)
            {
                throw new ServiceException(ServiceErrorCodes.AlreadyExists, 
                    $"User with id {id} already exists.");
            }
            user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new ServiceException(ServiceErrorCodes.AlreadyExists, 
                    $"User with email: '{email}' already exists.");
            }
            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);
            user = new User(id, username, email, hash, salt, role, firstName, lastName);
            await _userRepository.AddAsync(user);
        }
    }
}
