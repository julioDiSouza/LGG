using EncurtadorLink.Model.Token;

namespace EncurtadorLink.Services.Interface
{
    public interface ITokenService
    {
        public (ITokenModel, bool) ObterTokenJWT(TokenRequisicao requisicao, string ip);
    }
}
