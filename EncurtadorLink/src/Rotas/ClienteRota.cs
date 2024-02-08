using FluentValidation;
using EncurtadorLink.Model.Usuario;
using EncurtadorLink.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace EncurtadorLink.Rotas
{
    public static class ClienteRota
    {
        private static string _rota = string.Empty;
        public static void Cliente(this WebApplication app, string rota)
        {
            _rota = rota;
            app.CadastroCliente();
        }

        public static void CadastroCliente(this WebApplication app)
        {
            app.MapPost(_rota + "/cadastro", [AllowAnonymous] async (
                ClienteDtoCriar clienteDto,
                IClienteService clienteService,
                IValidator<ClienteDtoCriar> validacao
                ) =>
            {
                var resultado = await validacao.ValidateAsync(clienteDto);

                if (!resultado.IsValid)
                    return Results.ValidationProblem(resultado.ToDictionary());

                await clienteService.CadastrarAsync(clienteDto);

                return Results.Ok();
            });
        }
    }
}
