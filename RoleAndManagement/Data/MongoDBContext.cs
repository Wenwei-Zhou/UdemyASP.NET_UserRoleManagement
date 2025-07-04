using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleAndManagement.Data
{
    public interface IMongoDBContext
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string JobCollection { get; set; }
        string AuthicationCollection { get; set; }
    }

    public class MongoDBContext : IMongoDBContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string JobCollection { get; set; }
        public string AuthicationCollection { get; set; }
    }
}