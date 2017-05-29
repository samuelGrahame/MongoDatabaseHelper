using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDatabaseHelper
{
    public class ConnectionHandler<T> where T : BObject
    {
        public IMongoCollection<T> Collection { get; private set; }

        public ConnectionHandler(string connectionString, string database)
        {
            var client = new MongoClient(connectionString);

            var db = client.GetDatabase(database);

            Collection = db.GetCollection<T>(typeof(T).Name.ToLower() + "s");
        }
    }
}
