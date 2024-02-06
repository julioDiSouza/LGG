using EncurtadorLink.Model.Token;
using EncurtadorLink.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace EncurtadorLink.Endpoints
{
    public static class TokenRota
    {
        private static string _rota = string.Empty;
        public static void Token(this WebApplication app, string rota)
        {
            _rota = rota;
            app.Obter();
        }

        public static void Obter(this WebApplication app)
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

                var (token, sucesso) = tokenService.ObterTokenJWT(MontaRequisicao(httpContext), ip);

                if (!sucesso)
                    return Results.BadRequest(token);

                return Results.Ok(token);
            });
        }

        #region ..:: Privados ::..

        private static TokenRequisicao MontaRequisicao(HttpContext context)
        {
            return new TokenRequisicao
            {
                ClientId = context.Request.Headers["client_id"]!,
                ClientSecret = context.Request.Headers["client_secret"]!
            };
        }

        #endregion ..:: Fim - Privados ::..

    }
}
