using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.UseCases.Library.Services.Interfaces
{
    public interface ITokenService
    {
        public (ITokenModel, bool) ObterTokenAplicacao(RequisicaoTokenModel requisicao, string ip);
        Task<(ITokenModel, bool)> ObterTokenLoginAsync(RequisicaoLoginModel requisicao, string ip);
        Task LogoutAsync();
    }
}
