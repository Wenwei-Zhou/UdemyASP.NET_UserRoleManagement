// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using RoleAndManagement.Models;

// namespace RoleAndManagement.Service
// {
//     public interface IUserService<T> where T : class
//     {
//         Task<User> GetById(ObjectId id);
//         Task<User> GetByUsername(string username);
//         Task<User> GetByEmail(string email);
//         Task<User?> GetByEmailAsync(string email);
//     }
// }