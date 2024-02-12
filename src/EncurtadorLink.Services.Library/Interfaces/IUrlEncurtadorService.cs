using EncurtadorLink.Core.Library.Entidades.MySql;

namespace EncurtadorLink.Services.Library.Interfaces
{
    public interface IUrlEncurtadorService
    {
        Task<UrlEncurtada> GerarCodigoUnicoAsync(UrlEncurtada urlEncurtada);
        Task<UrlEncurtada> RecuperarLinkAsync(string codigo);
    }
}
