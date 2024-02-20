using System.Net;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using EncurtadorLink.Shared.Library.Models;
using Microsoft.Azure.Functions.Worker.Http;
using EncurtadorLink.Entities.Library.Mongo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EncurtadorLink.RecuperaLink.Function
{
    public class RecuperaLink
    {
        private static Lazy<MongoClient> lazyClient = new Lazy<MongoClient>(InitializeMongoClient());
        private MongoClient mongoClient = lazyClient.Value;
        private static MongoClient InitializeMongoClient()
        {
            return new MongoClient(Environment.GetEnvironmentVariable("MONGODB_HOST_URI"));
        }

        [Function("RecuperaLink")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            IMongoCollection<UrlMongoDbModel> colecao = mongoClient.GetDatabase("db_encurtador").GetCollection<UrlMongoDbModel>("urlMongoDB");
            var filtro = Builders<UrlMongoDbModel>.Filter.Eq(x => x.Codigo, "wsJh8") &
                         Builders<UrlMongoDbModel>.Filter.Eq(x => x.Ativo, true);

            var model = await colecao.Find(filtro).Project<UrlMongoDbModel>(Projecao).FirstOrDefaultAsync();

            return new OkObjectResult(model);
        }

        private ProjectionDefinition<UrlMongoDbModel> Projecao =>
            Builders<UrlMongoDbModel>.Projection
            .Include(x => x.Id)
            .Include(x => x.Codigo)
            .Include(x => x.UrlLonga)
            .Include(x => x.Ativo);
    }
}
