using Microsoft.AspNetCore.Authorization;

namespace EncurtadorLink.Rotas
{
    public static class TesteRota
    {
        private static string _rota= string.Empty;
        public static void Teste(this WebApplication app, string rota)
        {
            _rota= rota;
            app.Monitoramento();
            app.Autenticacao();
        }

        public static void Monitoramento(this WebApplication app)
        {
            app.MapGet(_rota + "/monitoramento", [AllowAnonymous] () => {
                return Results.Ok("Api no ar.");
            });
        }

        public static void Autenticacao(this WebApplication app)
        {
            app.MapGet(_rota + "/autenticacao", [Authorize] () => {
                return Results.Ok("Autenticado.");
            });
        }
    }
}