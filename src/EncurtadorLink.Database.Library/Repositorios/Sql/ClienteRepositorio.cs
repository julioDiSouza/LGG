using Microsoft.EntityFrameworkCore;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios.Sql
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly MysqlContext _dbContexto;

        public ClienteRepositorio(MysqlContext dbContexto)
        {
            _dbContexto = dbContexto;
        }

        public async Task<List<Cliente>> ListarClientesAsync()
        {
            return await _dbContexto.Cliente.AsNoTracking().ToListAsync();
        }
    }
}
