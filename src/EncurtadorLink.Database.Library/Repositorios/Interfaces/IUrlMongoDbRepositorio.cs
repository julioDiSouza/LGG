using EncurtadorLink.Entities.Library.Mongo;

namespace EncurtadorLink.Database.Library.Repositorios.Interfaces
{
    public interface IUrlMongoDbRepositorio
    {
        Task<UrlMongoDb> ConsultarPorCodigoAsync(string codigo);
    }
}
