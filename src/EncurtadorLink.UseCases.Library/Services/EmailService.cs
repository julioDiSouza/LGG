using SendGrid;
using AutoMapper;
using SendGrid.Helpers.Mail;
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
                await EnviarFilaEmailTwilioAsync(pendencia);
        }

        private async Task<List<FilaEmail>> ObterPendenciasAsync()
        {
            return await _udt.FilaEmailRepositorio.ObterPendenciasAsync();
        }

        private async Task EnviarFilaEmailTwilioAsync(FilaEmail filaEmail)
        {
            try
            {
                filaEmail.DataEnvio = Criptografador.BrazilDateTime();
                var twilio = new TwilioSendEmail();
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
            
                await twilio.SendEmailAsync(emailModel, _configuration);
                await _udt.FilaEmailRepositorio.RegistrarEnvioAsync(filaEmail);
                Console.WriteLine($"Email enviado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Falha ao enviar email: {ex.Message}");
            }
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

            await SendEmailAsync(emailModel);
            await _udt.FilaEmailRepositorio.RegistrarEnvioAsync(filaEmail);
        }

        private async Task SendEmailAsync(EmailModel emailModel)
        {
            var client = new SendGridClient(_configuration["EnvioEmail:Senha"]!);
            var from = new EmailAddress(_configuration["EnvioEmail:De"]!, "Links For You");
            var to = new EmailAddress(emailModel.To);
            var msg = MailHelper.CreateSingleEmail(from, to, emailModel.Subject, null, emailModel.Content);

            try
            {
                var response = await client.SendEmailAsync(msg);
                Console.WriteLine($"Email enviado.");
                Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Falha ao enviar email: {ex.Message}");
            }
        }
    }
}