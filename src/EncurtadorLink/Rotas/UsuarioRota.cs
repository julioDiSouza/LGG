using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.Rotas
{
    public static class UsuarioRota
    {
        private static string _rota = string.Empty;
        public static void Usuario(this WebApplication app, string rota)
        {
            _rota = rota;
            app.CadastroUsuario();
        }

        public static void CadastroUsuario(this WebApplication app)
        {
            app.MapPost(_rota + "/cadastro", [Authorize] async ( 
                UsuarioModelCriar usuarioDto, 
                IUsuarioService usuarioService, 
                IValidator<UsuarioModelCriar> validacao 
                ) =>
            {
                var resultado = await validacao.ValidateAsync(usuarioDto);

                if (!resultado.IsValid)
                    return Results.ValidationProblem(resultado.ToDictionary());

                var sucesso = await usuarioService.CadastrarAsync(usuarioDto);

                return sucesso ? Results.Ok() : Results.Problem("Falha ao cadastrar usuário");
            });
        }
    }
}
