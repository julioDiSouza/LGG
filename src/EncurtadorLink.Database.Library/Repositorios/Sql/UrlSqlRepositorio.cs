using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios.Sql
{
    public class UrlSqlRepositorio : SqlRepositorioBase<UrlSql>, IUrlSqlRepositorio
    {
        private readonly MysqlContext _dbContexto;

        public UrlSqlRepositorio(MysqlContext dbContexto) : base(dbContexto)
        {
            _dbContexto = dbContexto;
        }
    }
}