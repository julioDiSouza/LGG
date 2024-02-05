using EncurtadorLink.Entidades;

namespace EncurtadorLink.Services.Interface
{
    public interface IUrlEncurtadorService
    {
        Task<UrlEncurtada> GerarCodigoUnicoAsync(UrlEncurtada urlEncurtada);
        Task<UrlEncurtada> RecuperarLinkAsync(string codigo);
    }
}