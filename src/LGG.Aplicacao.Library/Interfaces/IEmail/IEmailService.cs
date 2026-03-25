using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Interfaces.IEmail
{
    public interface IEmailService
    {
        Task CriarFilaCadastroAsync(string usuarioId, string email);
        string CriarEmailKey(EmailConfirmacaoDto emailConfirmacao);
        (bool, EmailConfirmacaoDto?) ValidarKey(string key);
    }
}
