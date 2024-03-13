using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.UseCases.Library.Services.Interfaces
{
    public interface ITokenService
    {
        public (ITokenModel, bool) ObterTokenAplicacao(TokenModelRequisicao requisicao, string ip);
        Task<(ITokenModel, bool)> ObterTokenLoginAsync(LoginModelRequisicao requisicao, string ip);
        Task LogoutAsync();
    }
}
