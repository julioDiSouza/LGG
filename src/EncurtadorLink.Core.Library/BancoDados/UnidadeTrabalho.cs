using EncurtadorLink.Core.Library.BancoDados.Interfaces;
using EncurtadorLink.Core.Library.Repositorios.MongoDb;
using EncurtadorLink.Core.Library.Repositorios.MongoDb.Interfaces;

namespace EncurtadorLink.Core.Library.BancoDados
{
    public class UnidadeTrabalho : IUnidadeTrabalho
    {
        private readonly IMongoDbContext _mongoContexto;

        private IUrlMongoDBRepositorio _urlMongoDBRepositorio;

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

        public void Dispose()
        {
            _mongoContexto.Dispose();

            GC.SuppressFinalize(this);
        }

        public async Task StartTransactionAsync()
        {
            await _mongoContexto.StartTransactionAsync();
        }
    }
}
