using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.Shared.Library.Classes;
using EncurtadorLink.Shared.Library.Services.Interfaces;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.Rotas
{
    public static class UsuarioRota
    {
        private static string _rota = string.Empty;
        public static void Usuario(this WebApplication app, string rota)
        {
            _rota = rota;
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
                IEmailService emailService
                ) =>
            {
                if (string.IsNullOrWhiteSpace(model?.Email))
                    return Results.BadRequest(new MensagemRetornoModel(400, "Email inválido."));

                var usuario = await usuarioService.ConsultarPorEmailAsync(model.Email);

                if (usuario is null)
                    return Results.NotFound(new MensagemRetornoModel(404, "Email não encontrado."));

                var emailToken = Criptografador.RandomToken();

                usuario.ResetPasswordToken = emailToken;
                usuario.ResetPasswordDataExpiracao = Criptografador.BrazilDateTime().AddMinutes(15);

                await usuarioService.AtualizarAsync(usuario);

                /*
                emailService.SendEmail(
                    new EmailModel(
                        to: model.Email, 
                        subject: "l4u: Atualizar Senha.", 
                        content: EmailBody.EmailStringBody(model.Email, emailToken)
                        )
                    );
                */

                return Results.Ok();
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
