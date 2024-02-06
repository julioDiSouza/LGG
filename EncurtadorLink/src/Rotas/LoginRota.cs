using EncurtadorLink.Model.Usuario;
using EncurtadorLink.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace EncurtadorLink.Rotas
{
    public static class LoginRota
    {
        private static string _rota = string.Empty;
        public static void Login(this WebApplication app, string rota)
        {
            _rota = rota;
            app.CadastrarUsuario();
        }

        public static void CadastrarUsuario(this WebApplication app)
        {
            app.MapPost(_rota + "/cadastrar-usuario", [Authorize] async (
                UsuarioDtoCriar usuarioDto,
                IUsuarioService usuarioService 
                ) =>
            {
                var sucesso = await usuarioService.CadastrarUsuario(usuarioDto);

                return sucesso ? Results.NoContent() : Results.Problem("Falha ao cadastrar usuário");
            });
        }
    }
}
