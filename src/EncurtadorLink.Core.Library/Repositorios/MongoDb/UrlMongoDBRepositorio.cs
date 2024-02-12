using EncurtadorLink.Core.Library.Entidades.MongoDb;
using EncurtadorLink.Core.Library.BancoDados.Interfaces;
using EncurtadorLink.Core.Library.Repositorios.MongoDb.Interfaces;

namespace EncurtadorLink.Core.Library.Repositorios.MongoDb
{
    public class UrlMongoDBRepositorio : MongoRepositorioBase<UrlMongoDB>, IUrlMongoDBRepositorio
    {
        public UrlMongoDBRepositorio(IMongoDbContext contexto) : base(contexto, "urlMongoDB")
        {
            AplicarProjecao(x => new UrlMongoDB
            {
                Id = x.Id,
                Codigo = x.Codigo,
                UrlLonga = x.UrlLonga,
                Ativo = x.Ativo
            });
        }
    }
}
