using EncurtadorLink.Core.Library.BancoDados.Interfaces;
using EncurtadorLink.Core.Library.Repositorios.MongoDb.Interfaces;

namespace EncurtadorLink.Services.Library.UnidadeTrabalho.Interfaces
{
    public interface IUnidadeTrabalho : ITransactionalContext
    {
        IUrlMongoDBRepositorio UrlMongoDBRepositorio { get; }
    }
}
