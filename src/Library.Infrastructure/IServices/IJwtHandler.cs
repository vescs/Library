using Library.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.IServices
{
    public interface IJwtHandler : IService
    {
        JwtDTO CreateToken(Guid userId, string role);
    }
}
