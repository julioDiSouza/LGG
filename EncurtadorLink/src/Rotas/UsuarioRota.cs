using FluentValidation;
using EncurtadorLink.Model.Usuario;
using EncurtadorLink.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace EncurtadorLink.Rotas
{
    public static class UsuarioRota
    {
        private static string _rota = string.Empty;
        public static void Usuario(this WebApplication app, string rota)
        {
            _rota = rota;
            app.Cadastrar();
        }

        public static void Cadastrar(this WebApplication app)
        {
            app.MapPost(_rota + "/cadastrar", [Authorize] async ( 
                UsuarioDtoCriar usuarioDto, 
                IUsuarioService usuarioService, 
                IValidator<UsuarioDtoCriar> validacao 
                ) =>
            {
                var resultado = await validacao.ValidateAsync(usuarioDto);

                if (!resultado.IsValid)
                    return Results.ValidationProblem(resultado.ToDictionary());

                var sucesso = await usuarioService.CadastrarUsuario(usuarioDto);

                return sucesso ? Results.NoContent() : Results.Problem("Falha ao cadastrar usuário");
            });
        }
    }
}
