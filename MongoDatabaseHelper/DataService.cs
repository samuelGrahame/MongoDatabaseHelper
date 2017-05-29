using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDatabaseHelper
{
    public class DataService<T> where T : BObjectWithId
    {
        public readonly ConnectionHandler<T> Handler;

        public IEnumerable<T> Select(FilterDefinition<T> filder = null)
        {
            return Handler.Collection.Find<T>(filder ?? Builders<T>.Filter.Empty).ToEnumerable<T>();
        }

        public long Delete(FilterDefinition<T> filder = null)
        {
            return Handler.Collection.DeleteMany(filder ?? Builders<T>.Filter.Empty).DeletedCount;
        }

        public DataService(string connectionString, string database)
        {
            Handler = new ConnectionHandler<T>(connectionString, database);
        }

        public T Create(T businessObject)
        {
            this.Handler.Collection.InsertOne(businessObject);
            return businessObject;
        }

        public void Delete(Guid id)
        {
            this.Handler.Collection.DeleteOne(Builders<T>.Filter.Eq(bo => bo.Id, id));
        }

        public T Load(Guid id)
        {
            return this.Handler.Collection.Find(Builders<T>.Filter.Eq(bo => bo.Id, id)).FirstOrDefault();
        }

        public void Update(T businessObject)
        {
            this.Handler.Collection.ReplaceOne(Builders<T>.Filter.Eq(bo => bo.Id, businessObject.Id), businessObject);
        }
    }
}
