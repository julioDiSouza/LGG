using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace EncurtadorLink.Shared.Library.Models
{
    public class UrlMongoDbModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
        [JsonProperty("codigo")]
        public string Codigo { get; set; } = string.Empty;
        [JsonProperty("urlLogan")]
        public string UrlLonga { get; set; } = string.Empty;
        [JsonProperty("ativo")]
        public bool Ativo { get; set; }
    }
}
