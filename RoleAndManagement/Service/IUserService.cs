using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleAndManagement.Service
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterRequest request);
        Task<string?> AuthenticateAsync(string email, string password);
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByEmailAsync(string email);
    }
}