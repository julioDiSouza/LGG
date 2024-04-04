using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.Rotas
{
    public static class TokenRota
    {
        private static string _rota = string.Empty;
        public static void Token(this WebApplication app, string rota)
        {
            _rota = rota;
            app.ObterToken();
            app.ObterRefreshTokenAsync();
        }

        public static void ObterToken(this WebApplication app)
        {
            app.MapGet(_rota, [AllowAnonymous] (
                ITokenService tokenService,
                HttpContext httpContext
            ) =>
            {
                if (!httpContext.Request.Headers.ContainsKey("client_id") || 
                    !httpContext.Request.Headers.ContainsKey("client_secret"))
                    return Results.BadRequest("client_id e client_secret inválidos.");

                var (token, sucesso) = tokenService.ObterTokenAplicacao(MontaRequisicao(httpContext));

                if (!sucesso)
                    return Results.BadRequest(token);

                return Results.Ok(token);
            });
        }

        public static void ObterRefreshTokenAsync(this WebApplication app)
        {
            app.MapPost(_rota + "/refresh", [AllowAnonymous] async (
                [FromBody] TokenRefreshModelRequisicao requisicao, 
                ITokenService tokenService
            ) =>
            {
                var (token, sucesso) = await tokenService.ObterRefreshTokenAsync(requisicao);

                if (!sucesso)
                    return Results.BadRequest(token);

                return Results.Ok(token);
            });
        }

        #region ..:: Privados ::..

        private static TokenModelRequisicao MontaRequisicao(HttpContext context)
        {
            return new TokenModelRequisicao
            {
                ClientId = context.Request.Headers["client_id"]!,
                ClientSecret = context.Request.Headers["client_secret"]!
            };
        }

        #endregion ..:: Fim - Privados ::..
    }
}
