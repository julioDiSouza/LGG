using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using LGG.Aplicacao.Library.Dtos;
using Microsoft.Extensions.Configuration;
using LGG.Aplicacao.Library.Interfaces.IServices;

namespace LGG.Infraestrutura.Library.Services
{
    public class BrevoService : IBrevoService
    {
        private readonly IConfiguration _configuration;
        public BrevoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarEmailAsync(EmailMensagemDto emailModel)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("LGG", "juliodisouza@gmail.com"));
            message.To.Add(MailboxAddress.Parse(emailModel.Destino));
            //message.Cc.Add(MailboxAddress.Parse(email));
            //message.Bcc.Add(MailboxAddress.Parse(email));
            message.Subject = emailModel.Assunto;

            var builder = new BodyBuilder
            {
                HtmlBody = emailModel.Conteudo,
                TextBody = "Seu cliente de e-mail não suporta HTML."
            };

            message.Body = builder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp-relay.brevo.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("b362f4001@smtp-brevo.com", "xsmtpsib-d9fd227c70cc661e39d29b4f3068c035e725aa26fddc8b336083bc8581c52e01-j5OdOxlWzxK23f1b");
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
