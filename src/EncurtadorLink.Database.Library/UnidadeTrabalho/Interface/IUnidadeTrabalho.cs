using EncurtadorLink.Database.Library.Contextos.Interfaces;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;
using EncurtadorLink.Database.Library.Repositorios.Mongo.Interfaces;

namespace EncurtadorLink.Database.Library.UnidadeTrabalho.Interface
{
    public interface IUnidadeTrabalho : ITransactionalContext
    {
        IUrlMongoDbRepositorio UrlMongoDbRepositorio { get; }

        IClienteRepositorio ClienteRepositorio { get; }
        IUrlSqlRepositorio UrlSqlRepositorio { get; }
    }
}
