using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace EncurtadorLink.Database.Library.Contextos
{
    public class MongoDbContext
    {
        public IMongoDatabase DataBase { get; private set; }

        private readonly IMongoClient _mongoClient;

        public MongoDbContext(IConfiguration configuration)
        {
            var strConexao = configuration.GetConnectionString("DataBaseMongoDB");
            var url = new MongoUrl(strConexao);

            _mongoClient = new MongoClient(strConexao);
            _mongoClient.WithReadPreference(ReadPreference.SecondaryPreferred);

            DataBase = _mongoClient.GetDatabase(url.DatabaseName);
        }
    }
}
