using Microsoft.EntityFrameworkCore;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces;

namespace EncurtadorLink.Database.Library.Repositorios.Sql
{
    public class ClienteRepositorio2 : SqlRepositorioBase2<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio2(string connectionString) : base(connectionString)
        {

        }

        public Cliente? ConsultarClientePorId(string clienteId)
        {
            using (var db = Contexto())
            {
                return db.Cliente
                    .Include(c => c.Aplicacoes)
                    .FirstOrDefault(c =>
                        c.Id.ToString() == clienteId &&
                        c.Ativo == true
                    );
            }
        }

        public async Task<List<Cliente>> ListarClientesAsync()
        {
            using (var db = Contexto())
            {
                return await db.Cliente.AsNoTracking().ToListAsync();
            }
        }
    }
}
