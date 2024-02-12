using EncurtadorLink.Services.Library.Models;

namespace EncurtadorLink.Services.Library.Interfaces
{
    public interface ITokenService
    {
        public (ITokenModel, bool) ObterTokenAplicacao(RequisicaoTokenModel requisicao, string ip);
        Task<(ITokenModel, bool)> ObterTokenLoginAsync(RequisicaoLoginModel requisicao, string ip);
        Task LogoutAsync();
    }
}
