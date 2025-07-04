using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RoleAndManagement.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null; //这是在告诉 MongoDB 驱动：“虽然数据库里的 _id 是 ObjectId 类型，但请在程序中用 string 表示，我负责处理转换。”

        [BsonElement("username")]
        public string Username { get; set; } = null;

        [BsonElement("email")]
        public string Email { get; set; } = null;

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; } = null;

        // [BsonElement("roles")]
        // public List<string> Role { get; set; } = new();

        [BsonElement("roles")]
        public string Role { get; set; } = null;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}