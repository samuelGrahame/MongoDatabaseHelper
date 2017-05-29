using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDatabaseHelper
{
    public class BObjectWithId : BObject
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        public static implicit operator Guid(BObjectWithId boi) { return boi == null ? Guid.Empty : boi.Id; }
    }
}
