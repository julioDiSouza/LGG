using Microsoft.EntityFrameworkCore;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Database.Library.Contextos;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios.Sql
{
    public class ClienteRepositorio : SqlRepositorioBase<Cliente>, IClienteRepositorio
    {
        private readonly MysqlContext _dbContexto;

        public ClienteRepositorio(MysqlContext dbContexto) : base (dbContexto)
        {
            _dbContexto = dbContexto;
        }

        public Cliente? ConsultarClientePorIdAsync(string clienteId)
        {
            return _dbContexto.Cliente
                .Include(c => c.Aplicacoes)
                .FirstOrDefault(c => 
                    c.Id.ToString() == clienteId && 
                    c.Ativo == true 
                );
        }

        public async Task<List<Cliente>> ListarClientesAsync()
        {
            return await _dbContexto.Cliente.AsNoTracking().ToListAsync();
        }
    }
}
