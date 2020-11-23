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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        
        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
        }

        public async Task<TokenDTO> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }
            if (user.Password != password)
            {
                throw new Exception("Invalid credentials");
            }
            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
            return new TokenDTO
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role
            };
        }

        public async Task RegisterAsync(Guid id, string email, string username, string password, 
            string firstName, string lastName, string role = "user")
        {
            var user = await _userRepository.GetAsync(id);
            if (user != null)
            {
                throw new Exception($"User with id {id} already exists.");
            }
            user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new Exception($"User with email: '{email}' already exists.");
            }
            user = new User(id, username, email, password, role, firstName, lastName);
            await _userRepository.AddAsync(user);
        }
    }
}
