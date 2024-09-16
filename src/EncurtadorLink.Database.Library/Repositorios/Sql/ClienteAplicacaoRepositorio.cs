using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios.Sql
{
    public class ClienteAplicacaoRepositorio : SqlRepositorioBase<ClienteAplicacao>, IClienteAplicacaoRepositorio
    {
        private readonly MysqlContext _dbContexto;

        public ClienteAplicacaoRepositorio(MysqlContext dbContexto) : base(dbContexto)
        {
            _dbContexto = dbContexto;
        }
    }
}
