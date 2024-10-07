using Microsoft.Extensions.Configuration;
using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.Repositorios.Sql;
using EncurtadorLink.Database.Library.Repositorios.Mongo;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;
using EncurtadorLink.Database.Library.Repositorios.Mongo.Interfaces;

namespace EncurtadorLink.Database.Library.UnidadeTrabalho
{
    public class UnidadeTrabalho2 : IUnidadeTrabalho
    {
        private readonly MongoDbContext _mongoContexto;
        private readonly IConfiguration _configuration;
        private readonly string _mySqlConnection;

        private IUrlMongoDbRepositorio _urlMongoDbRepositorio;

        private IClienteRepositorio _clienteRepositorio;
        private IClienteAplicacaoRepositorio _clienteAplicacaoRepositorio;
        private IUrlSqlRepositorio _urlSqlRepositorio;
        private IFilaEmailRepositorio _filaEmailRepositorio;

        public UnidadeTrabalho2(MongoDbContext mongoContexto, IConfiguration configuration)
        {
            _mongoContexto = mongoContexto;
            _configuration = configuration;
            _mySqlConnection = configuration["ConnectionStrings:DataBaseMysql"]!;
        }

        public IUrlMongoDbRepositorio UrlMongoDbRepositorio =>
            _urlMongoDbRepositorio ?? new UrlMongoDbRepositorio(_mongoContexto);

        public IClienteRepositorio ClienteRepositorio =>
            _clienteRepositorio ?? new ClienteRepositorio2(_mySqlConnection);

        public IClienteAplicacaoRepositorio ClienteAplicacaoRepositorio =>
            _clienteAplicacaoRepositorio ?? new ClienteAplicacaoRepositorio2(_mySqlConnection);

        public IUrlSqlRepositorio UrlSqlRepositorio =>
            _urlSqlRepositorio ?? new UrlSqlRepositorio2(_mySqlConnection);

        public IFilaEmailRepositorio FilaEmailRepositorio =>
            _filaEmailRepositorio ?? new FilaEmailRepositorio2(_mySqlConnection);
    }
}
