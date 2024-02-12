using EncurtadorLink.Core.Library.Repositorios.MongoDb;
using EncurtadorLink.Core.Library.BancoDados.Interfaces;
using EncurtadorLink.Services.Library.UnidadeTrabalho.Interfaces;
using EncurtadorLink.Core.Library.Repositorios.MongoDb.Interfaces;

namespace EncurtadorLink.Services.Library.UnidadeTrabalho
{
    public class UnidadeTrabalho : IUnidadeTrabalho
    {
        private readonly IMongoDbContext _mongoContexto;

        private IUrlMongoDBRepositorio _urlMongoDBRepositorio;

        public UnidadeTrabalho(IMongoDbContext mongoContexto)
        {
            _mongoContexto = mongoContexto;
        }

        public IUrlMongoDBRepositorio UrlMongoDBRepositorio =>
            _urlMongoDBRepositorio ?? new UrlMongoDBRepositorio(_mongoContexto);

        public async Task AbortAsync()
        {
            await _mongoContexto.AbortAsync();
        }

        public async Task CommitAsync()
        {
            await _mongoContexto.CommitAsync();
        }

        public async Task StartTransactionAsync()
        {
            await _mongoContexto.StartTransactionAsync();
        }

        public void Dispose()
        {
            _mongoContexto.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
