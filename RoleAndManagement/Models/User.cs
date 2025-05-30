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
    public string Id { get; set; } = null;

    public string Username { get; set; } = null;
    public string Email { get; set; } = null;
    public string PasswordHash { get; set; } = null;
    public List<string> Roles { get; set; } = new();
    }
}