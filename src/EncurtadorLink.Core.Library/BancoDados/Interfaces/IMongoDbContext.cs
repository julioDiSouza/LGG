using MongoDB.Driver;

namespace EncurtadorLink.Core.Library.BancoDados.Interfaces
{
    public interface IMongoDbContext : ITransactionalContext<IClientSessionHandle>
    {
        IMongoDatabase DataBase { get; }
        bool IsProcessInTransaction();
    }
}
