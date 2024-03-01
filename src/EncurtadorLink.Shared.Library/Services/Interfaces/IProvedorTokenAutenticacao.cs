using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.Shared.Library.Services.Interfaces
{
    public interface IProvedorTokenAutenticacao
    {
        Task AtualizarTokenAsync(HttpResponseMessage response = null);
        TokenModel ObterToken();
        Task<string?> ObterTokenString();
    }
}
