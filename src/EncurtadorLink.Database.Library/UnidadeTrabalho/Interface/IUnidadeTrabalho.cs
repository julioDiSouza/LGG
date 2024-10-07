using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;
using EncurtadorLink.Database.Library.Repositorios.Mongo.Interfaces;

namespace EncurtadorLink.Database.Library.UnidadeTrabalho.Interface
{
    public interface IUnidadeTrabalho
    {
        IUrlMongoDbRepositorio UrlMongoDbRepositorio { get; }
        IClienteRepositorio ClienteRepositorio { get; }
        IClienteAplicacaoRepositorio ClienteAplicacaoRepositorio { get; }
        IUrlSqlRepositorio UrlSqlRepositorio { get; }
        IFilaEmailRepositorio FilaEmailRepositorio { get; }
    }
}
