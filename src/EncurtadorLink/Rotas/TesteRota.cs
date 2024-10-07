using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.Rotas
{
    public static class TesteRota
    {
        private static string _rota= string.Empty;
        public static void Teste(this WebApplication app, string rota)
        {
            _rota= rota;
            app.Ping();
            app.Autenticacao();
            app.AutenticacaoAdmin();
        }

        public static void Ping(this WebApplication app)
        {
            app.MapGet(_rota + "/ping", [AllowAnonymous] () => {
                return Results.Ok(new MensagemRetornoModel(200, "Api no ar."));
            });
        }

        public static void Autenticacao(this WebApplication app)
        {
            app.MapGet(_rota + "/autenticacao", [Authorize] () => {
                return Results.Ok(new MensagemRetornoModel(200, "Autenticado."));
            });
        }

        public static void AutenticacaoAdmin(this WebApplication app)
        {
            app.MapGet(_rota + "/autenticacao-admin", [Authorize(Roles = "Admin")] () => {
                return Results.Ok(new MensagemRetornoModel(200, "Autenticado. Olá administrador."));
            });
        }
    }
}