using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.Rotas
{
    public static class ClienteRota
    {
        private static string _rota = string.Empty;
        public static void Cliente(this WebApplication app, string rota)
        {
            _rota = rota;
            app.ListarClientes();
            app.CadastroCliente();
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
            app.MapPost(_rota + "/cadastro", [AllowAnonymous] async (
                ClienteModelCriar clienteModel,
                IClienteService clienteService,
                IValidator<ClienteModelCriar> validacao
                ) =>
            {
                try
                {
                    var resultado = await validacao.ValidateAsync(clienteModel);

                    if (!resultado.IsValid)
                        return Results.ValidationProblem(resultado.ToDictionary());

                    await clienteService.CadastrarAsync(clienteModel);
                }
                catch (Exception)
                {
                    return Results.Problem("Falha ao cadastrar cliente.");
                }

                return Results.Ok();
            });
        }
    }
}
