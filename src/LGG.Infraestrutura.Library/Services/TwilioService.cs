using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using LGG.Aplicacao.Library.Dtos;
using Microsoft.Extensions.Configuration;
using LGG.Aplicacao.Library.Interfaces.IServices;

namespace LGG.Infraestrutura.Library.Services
{
    public class TwilioService : ITwilioService
    {
        private readonly IConfiguration _configuration;
        public TwilioService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarEmailAsync(EmailMensagemDto emailModel)
        {
            using (var client = new SmtpClient())
            {
                using var message = new MimeMessage();
                message.From.Add(new MailboxAddress(
                    "LGG",
                    _configuration["EnvioEmail:De"]!
                ));
                message.To.Add(new MailboxAddress(
                    "LGG",
                    emailModel.Destino
                ));
                message.Subject = emailModel.Assunto;
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = emailModel.Conteudo
                };
                message.Body = bodyBuilder.ToMessageBody();

                await client.ConnectAsync("smtp.sendgrid.net", Convert.ToInt32(_configuration["EnvioEmail:Porta"]!), SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(
                    userName: "apikey",
                    password: _configuration["EnvioEmail:Senha"]!
                );

                await client.SendAsync(message);
            }
        }
    }
}