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
            app.Busca();
        }

        public static void Busca(this WebApplication app)
        {
            app.MapGet(_rota, [AllowAnonymous] (
            ITokenService tokenService,
            HttpContext httpContext
            ) =>
            {
                if (!httpContext.Request.Headers.ContainsKey("client_id") || 
                    !httpContext.Request.Headers.ContainsKey("client_secret"))
                    return Results.BadRequest("client_id e client_secret inválidos.");

                var ip = httpContext.Connection.RemoteIpAddress?.ToString()!;

                var (token, sucesso) = tokenService.ObterTokenAplicacao(MontaRequisicao(httpContext), ip);

                if (!sucesso)
                    return Results.BadRequest(token);

                return Results.Ok(token);
            });
        }

        #region ..:: Privados ::..

        private static RequisicaoTokenModel MontaRequisicao(HttpContext context)
        {
            return new RequisicaoTokenModel
            {
                ClientId = context.Request.Headers["client_id"]!,
                ClientSecret = context.Request.Headers["client_secret"]!
            };
        }

        #endregion ..:: Fim - Privados ::..

    }
}
