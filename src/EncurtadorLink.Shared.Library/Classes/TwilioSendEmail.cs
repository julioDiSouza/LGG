using EncurtadorLink.Shared.Library.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace EncurtadorLink.Shared.Library.Classes
{
    public class TwilioSendEmail
    {
        public async Task SendEmailAsync(EmailModel emailModel, IConfiguration configuration)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    using var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(
                        "L4u",
                        configuration["EnvioEmail:De"]!
                    ));
                    message.To.Add(new MailboxAddress(
                        "L4u",
                        emailModel.To
                    ));
                    message.Subject = emailModel.Subject;
                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = emailModel.Content
                    };
                    message.Body = bodyBuilder.ToMessageBody();

                    // SecureSocketOptions.StartTls force a secure connection over TLS
                    await client.ConnectAsync("smtp.sendgrid.net", Convert.ToInt32(configuration["EnvioEmail:Porta"]!), SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(
                        userName: "apikey", // the userName is the exact string "apikey" and not the API key itself.
                        password: configuration["EnvioEmail:Senha"]! // password is the API key
                    );

                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }
    }
}
