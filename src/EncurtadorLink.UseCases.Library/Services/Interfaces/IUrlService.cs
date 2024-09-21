using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.UseCases.Library.Services.Interfaces
{
    public interface IUrlService
    {
        Task<UrlSqlModel> GerarUrlCodigoUnicoAsync(UrlSqlModel urlSql);
        Task ApagarUrlAsync(Guid clienteId, Guid urlId);
        Task<ResultadoPaginadoModel<UrlSqlModelGrid>> ObterUrlsPaginadasAsync(Guid clienteId, int pagina);
    }
}
