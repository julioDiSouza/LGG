using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.Shared.Library.Services.Interfaces;

namespace EncurtadorLink.Shared.Library.Services
{
    public class EmailService2 : IEmailService2
    {
        private readonly IConfiguration _configuration;

        public EmailService2(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(EmailModel emailModel)
        {
            var emailMessage = new MimeMessage();
            var from = _configuration["EnvioEmail:De"]!;
            emailMessage.From.Add(new MailboxAddress("l4u", from));
            emailMessage.To.Add(new MailboxAddress(emailModel.To, emailModel.To));
            emailMessage.Subject = emailModel.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(emailModel.Content)
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration["EnvioEmail:SmtpServer"]!, int.Parse(_configuration["EnvioEmail:Porta"]!), true);
                    client.Authenticate(_configuration["EnvioEmail:De"]!, _configuration["EnvioEmail:Senha"]!);
                    client.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
