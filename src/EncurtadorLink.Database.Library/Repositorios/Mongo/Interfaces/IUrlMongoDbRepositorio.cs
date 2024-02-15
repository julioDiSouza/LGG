using EncurtadorLink.Entities.Library.Mongo;

namespace EncurtadorLink.Database.Library.Repositorios.Mongo.Interfaces
{
    public interface IUrlMongoDbRepositorio
    {
        Task AdicionarAsync(UrlMongoDb urlMongo);
        Task<UrlMongoDb> ConsultarPorCodigoAsync(string codigo);
    }
}
