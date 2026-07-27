using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Interfaces.IServices
{
    public interface IBrevoService
    {
        Task EnviarEmailAsync(EmailMensagemDto emailModel);
    }
}