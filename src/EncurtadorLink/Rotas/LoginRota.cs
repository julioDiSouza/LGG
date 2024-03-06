using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.Rotas
{
    public static class LoginRota
    {
        private static string _rota = string.Empty;
        public static void Login(this WebApplication app, string rota)
        {
            _rota = rota;
            app.LoginAsync();
            app.LogoutAsync();
        }

        public static void LoginAsync(this WebApplication app)
        {
            app.MapPost(_rota + "/login", [AllowAnonymous] async (
                RequisicaoLoginModel requisicao,
                ITokenService tokenService,
                HttpContext httpContext,
                IValidator<RequisicaoLoginModel> validacao
                ) =>
            {
                var resultado = await validacao.ValidateAsync(requisicao);

                if (!resultado.IsValid)
                    return Results.ValidationProblem(resultado.ToDictionary());

                var ip = httpContext.Connection.RemoteIpAddress?.ToString()!;

                var (token, sucesso) = await tokenService.ObterTokenLoginAsync(requisicao, ip);

                if (!sucesso)
                    return Results.BadRequest(token);

                return Results.Ok(token);
            });
        }

        public static void LogoutAsync(this WebApplication app)
        {
            app.MapGet(_rota + "/logout", [Authorize] async (
                ITokenService tokenService
                ) =>
            {
                await tokenService.LogoutAsync();

                return Results.Ok();
            });
        }
    }
}
