using LGG.Aplicacao.Library.Dtos;
using LGG.Dominio.Library.Entidades;

namespace LGG.Aplicacao.Library.Interfaces.IServices
{
    public interface IEmailService
    {
        Task<IEnumerable<EmailBalde>> ObterEmailsBaldeAsync();
        Task EnviarEmailSync(IEnumerable<EmailBalde> emailsBalde);
        string CriarEmailKey(EmailTokenDto emailToken);
        (bool, EmailTokenDto?) ValidarKey(string key);
    }
}