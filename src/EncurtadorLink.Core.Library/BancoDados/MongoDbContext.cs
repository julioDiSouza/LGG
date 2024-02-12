using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Core.Library.BancoDados.Interfaces;

namespace EncurtadorLink.Core.Library.BancoDados
{
    public class MongoDbContext : IMongoDbContext
    {
        public IMongoDatabase DataBase { get; private set; }
        public IClientSessionHandle Transaction { get; set; }

        private readonly IMongoClient _mongoClient;

        public MongoDbContext(IConfiguration configuration)
        {
            var strConexao = configuration.GetConnectionString("DataBaseMongoDB");
            var url = new MongoUrl(strConexao);

            _mongoClient = new MongoClient(strConexao);
            _mongoClient.WithReadPreference(ReadPreference.SecondaryPreferred);

            DataBase = _mongoClient.GetDatabase(url.DatabaseName);
        }

        public async Task AbortAsync()
        {
            if (Transaction == null || !Transaction.IsInTransaction)
                return;

            await Transaction.AbortTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (Transaction == null || !Transaction.IsInTransaction)
                return;

            await Transaction.CommitTransactionAsync();
        }

        public void Dispose()
        {
            if (Transaction != null && Transaction.IsInTransaction)
            {
                Transaction.AbortTransaction();
                Transaction.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        public bool IsProcessInTransaction()
        {
            return Transaction != null;
        }

        public async Task StartTransactionAsync()
        {
            Transaction = await _mongoClient.StartSessionAsync();
            Transaction.StartTransaction();
        }
    }
}
