using System.Net;
using System.Text.Json;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Extensoes;
using Microsoft.Extensions.Logging;
using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;
using LGG.Infraestrutura.Library.Email;
using LGG.Aplicacao.Library.Criptografia;
using Microsoft.Extensions.Configuration;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Interfaces.IServices;

namespace LGG.Infraestrutura.Library.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUnidTrabalho _ut;
        private readonly IBrevoService _brevo;

        private readonly string _emailCryptoKey;

        public EmailService(ILogger<EmailService> logger, IConfiguration configuration, 
            IUnidTrabalho ut, IBrevoService brevo)
        {
            _logger = logger;
            _configuration = configuration;
            _ut = ut;
            _brevo = brevo;
            _emailCryptoKey = configuration["Criptografia:Email:ChaveEmail"]!.ToString();
        }

        public async Task<IEnumerable<EmailBalde>> ObterEmailsBaldeAsync() =>
            await _ut.EmailBaldeRepositorio.ObterEmailsBaldeAsync(50);

        public async Task EnviarEmailSync(IEnumerable<EmailBalde> emailsBalde)
        {
            if (!emailsBalde.Any())
                return;

            foreach (var emailBalde in emailsBalde)
            {
                var destino = emailBalde.Email;
                var assunto = emailBalde.Tipo.ObterDescricao();
                var conteudo = string.Empty;

                var emailToken = new EmailTokenDto(
                    emailBalde.UsuarioId.ToString(),
                    emailBalde.Email,
                    emailBalde.Tipo.ObterDescricao());

                var token = CriarEmailKey(emailToken);
                token = WebUtility.UrlEncode(token);

                switch (emailBalde.Tipo)
                {
                    case EmailEnum.Confirmacao:
                        var endereco = _configuration["LGG:Endereco"]!.ToString();
                        conteudo = EmailConteudo.EmailConteudoCadastro(endereco, token);
                        break;
                    case EmailEnum.RecuperacaoSenha:
                        break;
                    case EmailEnum.CampeaoMensal:
                        break;
                    default:
                        throw new Exception("Tipo de email inválido.");
                }

                await _brevo.EnviarEmailAsync(new EmailMensagemDto(destino, assunto, conteudo));
            }
        }

        public string CriarEmailKey(EmailTokenDto emailToken)
        {
            var json = JsonSerializer.Serialize(emailToken);
            var token = Criptografador.EncryptString(json, _emailCryptoKey);

            return token;
        }

        public (bool, EmailTokenDto?) ValidarKey(string key)
        {
            bool valido = false;
            EmailTokenDto? emailConfirmacao = null;

            try
            {
                var json = Criptografador.DecryptString(key, _emailCryptoKey);

                if (!JsonExtensao.SafeDeserialize(json, _logger, out emailConfirmacao))
                    return (valido, emailConfirmacao);

                if (emailConfirmacao != null &&
                    emailConfirmacao.Mensagem == EmailEnum.Confirmacao.ObterDescricao())
                {
                    valido = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, key);
                return (valido, emailConfirmacao);
            }

            return (valido, emailConfirmacao);
        }
    }
}
