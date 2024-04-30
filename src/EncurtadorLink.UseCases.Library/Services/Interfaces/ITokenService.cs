using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.UseCases.Library.Services.Interfaces
{
    public interface ITokenService
    {
        public (ITokenModel, bool) ObterTokenAplicacao(TokenModelRequisicao requisicao);
        Task<(ITokenModel, bool)> ObterTokenLoginAsync(LoginModelRequisicao requisicao);
        Task<(ITokenModel, bool)> ObterTokenGoogleAsync(LoginModelGoogle model, IClienteService clienteService);
        Task<(ITokenModel, bool)> ObterRefreshTokenAsync(TokenRefreshModelRequisicao requisicao);
        Task LogoutAsync();
    }
}
