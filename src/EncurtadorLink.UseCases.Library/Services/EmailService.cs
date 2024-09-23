using MimeKit;
using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.Shared.Library.Classes;
using EncurtadorLink.Shared.Library.Enumeradores;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;

namespace EncurtadorLink.UseCases.Library.Services
{
    public class EmailService : IIEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnidadeTrabalho _udt;
        private readonly IMapper _mapper;

        public EmailService(IConfiguration configuration, IUnidadeTrabalho udt, IMapper mapper)
        {
            _configuration = configuration;
            _udt = udt;
            _mapper = mapper;
        }

        public async Task CriarFilaEmailAsync(FilaEmailModel model)
        {
            var filaEmail = _mapper.Map<FilaEmail>(model);

            await _udt.FilaEmailRepositorio.AdicionarAsync(filaEmail);
            await _udt.FilaEmailRepositorio.ApagarAnterioresRawAsync(filaEmail);
        }

        public async Task ProcessarFilaAsync()
        {
            var pendencias = await ObterPendenciasAsync();

            foreach (var pendencia in pendencias)
                await EnviarFilaEmailAsync(pendencia);
        }

        private async Task<List<FilaEmail>> ObterPendenciasAsync()
        {
            return await _udt.FilaEmailRepositorio.ObterPendenciasAsync();
        }

        private async Task EnviarFilaEmailAsync(FilaEmail filaEmail)
        {
            filaEmail.DataEnvio = Criptografador.BrazilDateTime();
            var assunto = filaEmail.Tipo == (int)FilaEmailEnum.Confirmacao ?
                "l4u: Confirmar Cadastro." :
                "l4u: Atualizar Senha.";

            var conteudo = filaEmail.Tipo == (int)FilaEmailEnum.Confirmacao ?
                EmailBody.EmailConfirmStringBody(_configuration["EnderecoUI"]!, filaEmail.Email, filaEmail.ConfirmationToken!) :
                EmailBody.EmailResetStringBody(_configuration["EnderecoUI"]!, filaEmail.Email, filaEmail.ResetPasswordToken!);

            var emailModel = new EmailModel(
                to: filaEmail.Email,
                subject: assunto,
                content: conteudo
            );

            //await SendEmailAsync(emailModel);
            await _udt.FilaEmailRepositorio.RegistrarEnvioAsync(filaEmail);
        }

        private async Task SendEmailAsync(EmailModel emailModel)
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
                    await client.SendAsync(emailMessage);
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