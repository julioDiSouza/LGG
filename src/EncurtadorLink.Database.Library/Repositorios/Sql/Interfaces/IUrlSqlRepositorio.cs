using EncurtadorLink.Entities.Library.Sql;

namespace EncurtadorLink.Database.Library.Repositorios.Sql.Interfaces
{
    public interface IUrlSqlRepositorio : ISqlRepositorioBase<UrlSql>
    {
        Task<(int, List<UrlSql>)> ObterUrlsPaginadasAsync(Guid clienteId, int pagina, int quantidade);
        Task<UrlSql> ApagarUrlAsync(Guid clienteId, Guid urlId);
    }
}