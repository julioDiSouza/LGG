using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.UseCases.Library.Services
{
    public class UrlMongoDbLazyService : IUrlMongoDbLazyService
    {
        #region ..:: Construtor e variaveis ::..

        private readonly IMongoCollection<UrlMongoDbModel> _colecao;

        public UrlMongoDbLazyService(IConfiguration configuration)
        {
            var lazyClient = new Lazy<MongoClient>(new MongoClient(configuration.GetConnectionString("DataBaseMongoDb")));
            var mongoClient = lazyClient.Value;

            _colecao = mongoClient.GetDatabase(configuration["Database"]!).GetCollection<UrlMongoDbModel>("urlMongoDB");
        }

        #endregion ..:: Fim Construtor e variaveis ::..

        public async Task<UrlMongoDbModel> ConsultarPorCodigoAsync(string codigo)
        {
            var filtro = Builders<UrlMongoDbModel>.Filter.Eq(x => x.Codigo, codigo) &
                         Builders<UrlMongoDbModel>.Filter.Eq(x => x.Ativo, true);

            return await _colecao.Find(filtro).Project<UrlMongoDbModel>(Projecao).FirstOrDefaultAsync();
        }

        #region ..:: Privados ::..

        private static ProjectionDefinition<UrlMongoDbModel> Projecao => 
            Builders<UrlMongoDbModel>.Projection
            .Include(x => x.Id)
            .Include(x => x.Codigo)
            .Include(x => x.UrlLonga)
            .Include(x => x.Ativo);

        #endregion ..:: Fim Privados ::..
    }
}
