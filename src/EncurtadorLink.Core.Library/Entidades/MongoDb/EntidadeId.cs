using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EncurtadorLink.Core.Library.Entidades.MongoDb
{
    public class EntidadeId
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
    }
}
