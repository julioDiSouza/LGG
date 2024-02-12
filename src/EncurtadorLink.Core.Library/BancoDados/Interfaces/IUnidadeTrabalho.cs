using EncurtadorLink.Core.Library.Repositorios.MongoDb.Interfaces;

namespace EncurtadorLink.Core.Library.BancoDados.Interfaces
{
    public interface IUnidadeTrabalho : ITransactionalContext
    {
        IUrlMongoDBRepositorio UrlMongoDBRepositorio { get; }
    }
}
