using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios.Sql
{
    public class FilaEmailRepositorio : SqlRepositorioBase<FilaEmail>, IFilaEmailRepositorio
    {
        private readonly MysqlContext _dbContexto;

        public FilaEmailRepositorio(MysqlContext dbContexto) : base(dbContexto)
        {
            _dbContexto = dbContexto;
        }
    }
}