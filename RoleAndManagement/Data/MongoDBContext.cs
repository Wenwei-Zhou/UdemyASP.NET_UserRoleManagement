using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleAndManagement.Data
{
    public class MongoDBContext
    {
        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string JobCollection { get; set; } = null;
        public string AuthicationCollection { get; set; } = null;
    }
}