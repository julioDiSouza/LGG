using EncurtadorLink.Entities.Library.Sql;

namespace EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<List<Cliente>> ListarClientesAsync();
    }
}
