using EncurtadorLink.Model.Token;
using EncurtadorLink.Model.Requisicao;

namespace EncurtadorLink.Services.Interface
{
    public interface ITokenService
    {
        public (ITokenModel, bool) ObterTokenAplicacao(TokenRequisicaoModel requisicao, string ip);
        Task<(ITokenModel, bool)> ObterTokenLoginAsync(LoginRequisicaoModel requisicao, string ip);
        Task LogoutAsync();
    }
}
