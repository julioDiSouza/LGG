using EncurtadorLink.Entities.Library.Sql;

namespace EncurtadorLink.UseCases.Library.Services.Interfaces
{
    public interface IUrlService
    {
        Task<UrlSql> GerarUrlCodigoUnicoAsync(UrlSql urlSql);
    }
}
