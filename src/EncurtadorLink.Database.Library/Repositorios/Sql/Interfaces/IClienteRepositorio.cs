using EncurtadorLink.Entities.Library.Sql;

namespace EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces
{
    public interface IClienteRepositorio : ISqlRepositorioBase<Cliente>
    {
        Cliente? ConsultarClientePorIdAsync(string clienteId);
        Task<List<Cliente>> ListarClientesAsync();
    }
}
