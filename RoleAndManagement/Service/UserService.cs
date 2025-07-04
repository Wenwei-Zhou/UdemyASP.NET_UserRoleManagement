using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;
using RoleAndManagement.Models;
using RoleAndManagement.Data;
using RoleAndManagement.Service;

namespace RoleAndManagement.Service
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IMongoDBContext _context)
        {
            var client = new MongoClient(_context.ConnectionString);
            var database = client.GetDatabase(_context.DatabaseName);
            _users = database.GetCollection<User>(_context.AuthicationCollection);
        }

        public async Task<User> GetById(string id)
        {
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _users.Find(user => user.Username == username).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _users.Find(user => user.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> Create(User user, string password)
        {
            // 检查用户名和邮箱是否已存在
            if (await GetByUsername(user.Username) != null ||
                await GetByEmail(user.Email) != null)
            {
                return false;
            }

            // 创建密码哈希
            user.PasswordHash = HashPassword(password);
            
            await _users.InsertOneAsync(user);
            return true;
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            return storedHash == HashPassword(password);
        }

        // 简单的密码哈希方法
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}