using Library.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.IServices
{
    public interface IUserService : IService
    {
        Task<UserDTO> GetUserInfoAsync(Guid id);
        Task RegisterAsync(Guid id, string email, string name, string password, 
            string firstName, string lastName, string role = "user");
        Task<TokenDTO> LoginAsync(string email, string password);
    }
}
