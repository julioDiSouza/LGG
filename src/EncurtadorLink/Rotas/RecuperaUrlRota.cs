using EncurtadorLink.Shared.Library;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.UseCases.Library.Services;

namespace EncurtadorLink.Rotas
{
    public static class RecuperaUrlRota
    {
        private static string _rota = string.Empty;
        public static void RecuperaUrl(this WebApplication app, string rota)
        {
            _rota = rota;
            app.RecuperarUrlLonga();
        }

        public static void RecuperarUrlLonga(this WebApplication app)
        {
            app.MapGet(_rota + "{codigo}", [AllowAnonymous] async (IConfiguration configuration, string codigo) => 
            {
                if (codigo.Length == Constantes.TAMANHO_CODIGO_UNICO)
                {
                    try
                    {
                        var urlMongo = 
                        await new UrlMongoDbLazyService(configuration)
                        .ConsultarPorCodigoAsync(codigo);

                        if (urlMongo is not null)
                            return Results.Redirect(urlMongo.UrlLonga);
                    }
                    catch (Exception) { }
                }

                return Results.NotFound();
            });
        }
    }
}
