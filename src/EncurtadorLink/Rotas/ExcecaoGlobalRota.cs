using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.Shared.Library.Classes;

namespace EncurtadorLink.Rotas
{
    public static class ExcecaoGlobalRota
    {
        private readonly static string _mensagemErroGenerico = "Erro interno, tente novamente mais tarde.";
        private static string _rota = string.Empty;

        public static void ExcecaoGlobal(this WebApplication app, string rota)
        {
            _rota = rota;
            app.Excecao();
        }

        public static void Excecao(this WebApplication app)
        {
            app.Map(_rota , [AllowAnonymous]  (
                ILogger<LogGenerico> logger, 
                HttpContext httpContext 
                ) => 
            {
                try
                {
                    var exceptionHandlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>();
                    if (exceptionHandlerPathFeature?.Error is not null)
                        logger.LogError($"Erro: {exceptionHandlerPathFeature!.Error.Message}.");

                    return Results.Problem(_mensagemErroGenerico);
                }
                catch (Exception)
                {
                    return Results.Problem(_mensagemErroGenerico);
                }
            });
        }
    }
}
