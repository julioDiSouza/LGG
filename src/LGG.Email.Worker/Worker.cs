using System.Web;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Extensoes;
using LGG.Global.Library.Enumeradores;
using LGG.Infraestrutura.Library.Email;
using LGG.Aplicacao.Library.Interfaces.IRabbitMq;
using LGG.Aplicacao.Library.Interfaces.IServices;

namespace LGG.Email.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;
        private readonly IRabbitMqConsumer _consumer;
        private readonly IEmailService _emailService;
        private readonly string _filaCadastro;
        private readonly string _lggEndereco;
        private readonly string _filaCryptoKey;

        public Worker(IConfiguration configuration, ILogger<Worker> logger, 
            IRabbitMqConsumer consumer, IEmailService emailService)
        {
            _configuration = configuration;
            _logger = logger;
            _consumer = consumer;
            _emailService = emailService;
            _filaCadastro = _configuration["RabbitMqFilas:CadastroEmail"]!.ToString();
            _lggEndereco = _configuration["LGG:Endereco"]!.ToString();
            _filaCryptoKey = _configuration["Criptografia:Fila:ChaveFila"]!.ToString();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.StartAsync(
                _filaCadastro,
                ProcessarAsync,
                stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private async Task<MsgResultadoEnum> ProcessarAsync(string mensagem)
        {
            try
            {
                _logger.LogInformation($"Recebido mensagem: {mensagem}");

                if (!JsonExtensao.SafeDeserialize(mensagem, _logger, out EmailCadastroConfirmacaoDtoMsg? msgEmail))
                    return MsgResultadoEnum.Discard;

                if (!msgEmail!.ValidarMensagem(_filaCryptoKey))
                    return MsgResultadoEnum.Discard;

                string url = $"{_lggEndereco}/Email/cadastro_confirmacao";
                var emailConfirmacao = new EmailConfirmacaoDto(msgEmail.UsuarioId, msgEmail.Email, EmailEnum.EmailValidacao.ObterDescricao());
                string emailKey = _emailService.CriarEmailKey(emailConfirmacao!);
                string html = EmailConteudo.EmailConteudoCadastro(url, HttpUtility.UrlEncode(emailKey));

                // Gravando arquivo para a finalidade de teste.
                string path = "C:\\LGG\\Email\\arquivo.html";
                File.WriteAllText(path, html);

                await Task.CompletedTask;

                return MsgResultadoEnum.Ack;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, mensagem);

                return MsgResultadoEnum.Discard;
            }
        }
    }
}