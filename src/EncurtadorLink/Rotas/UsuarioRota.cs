using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.Shared.Library.Classes;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.Shared.Library.Enumeradores;
using System.Web;

namespace EncurtadorLink.Rotas
{
    public static class UsuarioRota
    {
        private static string _rota = string.Empty;
        private static IConfiguration _configuration;
        public static void Usuario(this WebApplication app, string rota, IConfiguration configuration)
        {
            _rota = rota;
            _configuration = configuration;
            app.CadastrarUsuarioAsync();
            app.EmailRecuperacaoSenhaAsync();
            app.NovaSenhaAsync();
        }

        public static void CadastrarUsuarioAsync(this WebApplication app)
        {
            app.MapPost(_rota , [Authorize] async ( 
                UsuarioModelCriar usuarioModel, 
                IUsuarioService usuarioService, 
                IValidator<UsuarioModelCriar> validacao 
                ) =>
            {
                var resultado = await validacao.ValidateAsync(usuarioModel);

                if (!resultado.IsValid)
                    return Results.ValidationProblem(resultado.ToDictionary());

                var sucesso = await usuarioService.CadastrarAsync(usuarioModel);

                return sucesso ? Results.Ok() : Results.BadRequest(new MensagemRetornoModel(400, "Falha ao cadastrar usuario."));
            });
        }

        public static void EmailRecuperacaoSenhaAsync(this WebApplication app)
        {
            app.MapPost(_rota + "/atualizacao-senha", async (
                EmailModelRequisicao model,
                IUsuarioService usuarioService,
                IIEmailService emailService
                ) =>
            {
                if (string.IsNullOrWhiteSpace(model?.Email))
                    return Results.BadRequest(new MensagemRetornoModel(400, "Email inválido."));

                var usuario = await usuarioService.ConsultarPorEmailAsync(model.Email);

                if (usuario is null)
                    return Results.Ok(new MensagemRetornoModel(404, "Email não cadastrado."));

                var emailToken = Criptografador.RandomToken();
                var dataExpiracao = Criptografador.BrazilDateTime().AddMinutes(15);

                usuario.ResetPasswordToken = emailToken;
                usuario.ResetPasswordDataExpiracao = dataExpiracao;

                await usuarioService.AtualizarAsync(usuario);

                var filaEmail = new FilaEmailModel();
                filaEmail.Id = Guid.NewGuid();
                filaEmail.ClienteId = usuario.ClienteId;
                filaEmail.Email = model.Email;
                filaEmail.ResetPasswordToken = HttpUtility.UrlEncode(Criptografador.EncryptToString("__valido__" + emailToken, _configuration["CryptoKey"]!));
                filaEmail.ResetPasswordDataExpiracao = dataExpiracao;
                filaEmail.DataInclusao = Criptografador.BrazilDateTime();
                filaEmail.Tipo = FilaEmailEnum.ResetSenha;

                await emailService.CriarFilaEmailAsync(filaEmail);  

                /*
                emailService.SendEmail(
                    new EmailModel(
                        to: model.Email, 
                        subject: "l4u: Atualizar Senha.", 
                        content: EmailBody.EmailStringBody(model.Email, emailToken)
                        )
                    );
                */

                return Results.Ok(new MensagemRetornoModel(200, "Email enviado."));
            });
        }

        public static void NovaSenhaAsync(this WebApplication app)
        {
            app.MapPost(_rota + "/nova-senha", async (
                AtualizaSenhaModel model,
                IUsuarioService usuarioService,
                IValidator<AtualizaSenhaModel> validacao
                ) =>
            {
                var resultado = await validacao.ValidateAsync(model);

                if (!resultado.IsValid)
                    return Results.ValidationProblem(resultado.ToDictionary());

                if (model.NovaSenha != model.ReNovaSenha)
                    return Results.BadRequest(new MensagemRetornoModel(400, "Confirmação de senha inválida."));

                var usuario = await usuarioService.ConsultarPorEmailAsync(model.Email);

                if (usuario is null)
                    return Results.NotFound(new MensagemRetornoModel(404, "Usuário não encontrado."));

                if (usuario.ResetPasswordToken != model.EmailToken || usuario.ResetPasswordDataExpiracao < Criptografador.BrazilDateTime())
                    return Results.BadRequest(new MensagemRetornoModel(400, "Link encerrado, solicite um novo."));

                usuario.ResetPasswordDataExpiracao = Criptografador.BrazilDateTime();

                await usuarioService.AtualizarSenhaAsync(usuario, model.NovaSenha);

                return Results.Ok();
            });
        }
    }
}
