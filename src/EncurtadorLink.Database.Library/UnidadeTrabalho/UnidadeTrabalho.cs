using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.Repositorios.Sql;
using EncurtadorLink.Database.Library.Repositorios.Mongo;
using EncurtadorLink.Database.Library.Contextos.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;
using EncurtadorLink.Database.Library.Repositorios.Mongo.Interfaces;

namespace EncurtadorLink.Database.Library.UnidadeTrabalho
{
    public class UnidadeTrabalho : IUnidadeTrabalho
    {
        private readonly IMongoDbContext _mongoContexto;
        private readonly MysqlContext _mysqlContexto;

        private IUrlMongoDbRepositorio _urlMongoDbRepositorio;
        
        private IClienteRepositorio _clienteRepositorio;

        public UnidadeTrabalho(IMongoDbContext mongoContexto, MysqlContext mysqlContexto)
        {
            _mongoContexto = mongoContexto;
            _mysqlContexto = mysqlContexto;
        }

        public IUrlMongoDbRepositorio UrlMongoDbRepositorio =>
            _urlMongoDbRepositorio ?? new UrlMongoDbRepositorio(_mongoContexto);

        public IClienteRepositorio ClienteRepositorio =>
            _clienteRepositorio ?? new ClienteRepositorio(_mysqlContexto);

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
