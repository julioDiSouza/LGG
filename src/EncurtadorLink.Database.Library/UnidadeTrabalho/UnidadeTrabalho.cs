using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.Repositorios.Sql;
using EncurtadorLink.Database.Library.Repositorios.Mongo;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;
using EncurtadorLink.Database.Library.Repositorios.Mongo.Interfaces;

namespace EncurtadorLink.Database.Library.UnidadeTrabalho
{
    public class UnidadeTrabalho : IUnidadeTrabalho
    {
        private readonly MongoDbContext _mongoContexto;
        private readonly MysqlContext _mysqlContexto;

        private IUrlMongoDbRepositorio _urlMongoDbRepositorio;
        
        private IClienteRepositorio _clienteRepositorio;
        private IClienteAplicacaoRepositorio _clienteAplicacaoRepositorio;
        private IUrlSqlRepositorio _urlSqlRepositorio;
        private IFilaEmailRepositorio _filaEmailRepositorio;

        public UnidadeTrabalho(MongoDbContext mongoContexto, MysqlContext mysqlContexto)
        {
            _mongoContexto = mongoContexto;
            _mysqlContexto = mysqlContexto;
        }

        public IUrlMongoDbRepositorio UrlMongoDbRepositorio =>
            _urlMongoDbRepositorio ?? new UrlMongoDbRepositorio(_mongoContexto);

        public IClienteRepositorio ClienteRepositorio =>
            _clienteRepositorio ?? new ClienteRepositorio(_mysqlContexto);

        public IClienteAplicacaoRepositorio ClienteAplicacaoRepositorio =>
            _clienteAplicacaoRepositorio ?? new ClienteAplicacaoRepositorio(_mysqlContexto);

        public IUrlSqlRepositorio UrlSqlRepositorio => 
            _urlSqlRepositorio ?? new UrlSqlRepositorio(_mysqlContexto);

        public IFilaEmailRepositorio FilaEmailRepositorio =>
            _filaEmailRepositorio ?? new FilaEmailRepositorio(_mysqlContexto);
    }
}
