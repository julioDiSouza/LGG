using System.Text.Json;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Extensoes;
using Microsoft.Extensions.Logging;
using LGG.Global.Library.Enumeradores;
using LGG.Aplicacao.Library.Criptografia;
using Microsoft.Extensions.Configuration;
using LGG.Aplicacao.Library.Interfaces.IRabbitMq;
using LGG.Aplicacao.Library.Interfaces.IServices;

namespace LGG.Infraestrutura.Library.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        private readonly IRabbitMqPublisher _publisher;
        private readonly string _filaKey;
        private readonly string _filaCadastro;
        private readonly string _emailCryptoKey;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger, IRabbitMqPublisher publisher)
        {
            _configuration = configuration;
            _logger = logger;
            _publisher = publisher;
            _filaKey = _configuration["Criptografia:Fila:ChaveFila"]!.ToString();
            _filaCadastro = _configuration["RabbitMqFilas:CadastroEmail"]!.ToString();
            _emailCryptoKey = _configuration["Criptografia:Email:ChaveEmail"]!.ToString();
        }

        public async Task CriarFilaCadastroAsync(string usuarioId, string email)
        {
            var filaEmail = new EmailCadastroConfirmacaoDtoMsg(_filaKey);
            filaEmail.UsuarioId = usuarioId;
            filaEmail.Email = email;

            await _publisher.PublishAsync(_filaCadastro, filaEmail);
        }

        public string CriarEmailKey(EmailConfirmacaoDto msgEmail)
        {
            var json = JsonSerializer.Serialize(msgEmail);
            var token = Criptografador.EncryptString(json, _emailCryptoKey);

            return token;
        }

        public (bool, EmailConfirmacaoDto?) ValidarKey(string key)
        {
            bool valido = false;
            EmailConfirmacaoDto? emailConfirmacao = null;

            try
            {
                var json = Criptografador.DecryptString(key, _emailCryptoKey);

                if (!JsonExtensao.SafeDeserialize(json, _logger, out emailConfirmacao))
                    return (valido, emailConfirmacao);

                if (emailConfirmacao != null && 
                    emailConfirmacao.Frase == EmailEnum.Confirmacao.ObterDescricao())
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
