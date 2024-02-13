using EncurtadorLink.Entities.Library.Mongo;
using EncurtadorLink.Database.Library.Contextos.Interfaces;
using EncurtadorLink.Database.Library.Repositorios.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios
{
    public class UrlMongoDbRepositorio : MongoRepositorioBase<UrlMongoDb>, IUrlMongoDbRepositorio
    {
        public UrlMongoDbRepositorio(IMongoDbContext contexto) : base(contexto, "urlMongoDB")
        {
            AplicarProjecao(x => new UrlMongoDb
            {
                Id = x.Id,
                Codigo = x.Codigo,
                UrlLonga = x.UrlLonga,
                Ativo = x.Ativo
            });
        }
    }
}
