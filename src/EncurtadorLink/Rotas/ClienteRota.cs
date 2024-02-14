using FluentValidation;
using EncurtadorLink.Model.Usuario;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EncurtadorLink.Rotas
{
    public static class ClienteRota
    {
        private static string _rota = string.Empty;
        public static void Cliente(this WebApplication app, string rota)
        {
            _rota = rota;
            app.CadastroCliente();
            app.ListarClientes();
        }

        public static void ListarClientes(this WebApplication app)
        {
            app.MapGet(_rota , [AllowAnonymous] async ([FromServices] IClienteService clienteService) => 
            {
                try
                {
                    var clientes = await clienteService.ListarClientesAsync();

                    if (clientes is not null)
                        return Results.Ok(clientes);
                }
                catch (Exception) { }

                return Results.NotFound();
            });
        }

        public static void CadastroCliente(this WebApplication app)
        {
            /*
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
            */
        }
    }
}
