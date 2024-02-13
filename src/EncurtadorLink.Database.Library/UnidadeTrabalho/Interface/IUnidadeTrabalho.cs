using EncurtadorLink.Database.Library.Contextos.Interfaces;
using EncurtadorLink.Database.Library.Repositorios.Interfaces;

namespace EncurtadorLink.Database.Library.UnidadeTrabalho.Interface
{
    public interface IUnidadeTrabalho : ITransactionalContext
    {
        IUrlMongoDbRepositorio UrlMongoDBRepositorio { get; }
    }
}
