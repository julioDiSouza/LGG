using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.UseCases.Library.Services.Interfaces
{
    public interface IUrlService
    {
        Task<UrlSqlModel> GerarUrlCodigoUnicoAsync(UrlSqlModel urlSql);
    }
}
