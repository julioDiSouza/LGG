using EncurtadorLink.Database.Library.Repositorios;
using EncurtadorLink.Database.Library.Contextos.Interfaces;
using EncurtadorLink.Database.Library.Repositorios.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;

namespace EncurtadorLink.Database.Library.UnidadeTrabalho
{
    public class UnidadeTrabalho : IUnidadeTrabalho
    {
        private readonly IMongoDbContext _mongoContexto;

        private IUrlMongoDbRepositorio _urlMongoDbRepositorio;

        public UnidadeTrabalho(IMongoDbContext mongoContexto)
        {
            _mongoContexto = mongoContexto;
        }

        public IUrlMongoDbRepositorio UrlMongoDbRepositorio =>
            _urlMongoDbRepositorio ?? new UrlMongoDbRepositorio(_mongoContexto);

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
