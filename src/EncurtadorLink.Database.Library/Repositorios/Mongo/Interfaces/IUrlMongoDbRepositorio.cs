using EncurtadorLink.Entities.Library.Mongo;

namespace EncurtadorLink.Database.Library.Repositorios.Mongo.Interfaces
{
    public interface IUrlMongoDbRepositorio
    {
        Task<UrlMongoDb> ConsultarPorCodigoAsync(string codigo);
    }
}
