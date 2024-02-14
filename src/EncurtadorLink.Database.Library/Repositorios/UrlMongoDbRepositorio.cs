using MongoDB.Driver;
using EncurtadorLink.Entities.Library.Mongo;
using EncurtadorLink.Database.Library.Contextos.Interfaces;
using EncurtadorLink.Database.Library.Repositorios.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios
{
    public class UrlMongoDbRepositorio : IUrlMongoDbRepositorio
    {
        private readonly IMongoCollection<UrlMongoDb> _colecao;

        public UrlMongoDbRepositorio(IMongoDbContext contexto)
        {
            _colecao = contexto.DataBase.GetCollection<UrlMongoDb>("urlMongoDB");
        }

        public async Task<UrlMongoDb> ConsultarPorCodigoAsync(string codigo)
        {
            var filtro = Builders<UrlMongoDb>.Filter.Eq(x => x.Codigo, codigo) & 
                         Builders<UrlMongoDb>.Filter.Eq(x => x.Ativo, true);
            
            return await _colecao.Find(filtro).Project<UrlMongoDb>(Projecao).FirstOrDefaultAsync();
        }

        private static ProjectionDefinition<UrlMongoDb> Projecao =>
            Builders<UrlMongoDb>.Projection
            .Include(x => x.Id)
            .Include(x => x.Codigo)
            .Include(x => x.UrlLonga)
            .Include(x => x.Ativo);
    }
}
