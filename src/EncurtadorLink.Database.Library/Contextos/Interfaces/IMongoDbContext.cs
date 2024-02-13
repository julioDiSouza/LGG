using MongoDB.Driver;

namespace EncurtadorLink.Database.Library.Contextos.Interfaces
{
    public interface IMongoDbContext : ITransactionalContext<IClientSessionHandle>
    {
        IMongoDatabase DataBase { get; }
        bool IsProcessInTransaction();
    }
}
