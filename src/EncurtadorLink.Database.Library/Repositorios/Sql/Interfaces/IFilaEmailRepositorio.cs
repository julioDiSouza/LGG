using EncurtadorLink.Entities.Library.Sql;

namespace EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces
{
    public interface IFilaEmailRepositorio : ISqlRepositorioBase<FilaEmail>
    {
        Task<List<FilaEmail>> ObterPendenciasAsync();
        Task RegistrarEnvioAsync(FilaEmail filaEmail);
        Task ApagarAnterioresAsync(FilaEmail filaEmail);
        Task ApagarAnterioresRawAsync(FilaEmail filaEmail);
    }
}