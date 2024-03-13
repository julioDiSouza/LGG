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
            app.ListarClientesAsync();
            app.CadastrarClienteAsync();
        }

        public static void ListarClientesAsync(this WebApplication app)
        {
            app.MapGet(_rota , [Authorize] async ([FromServices] IClienteService clienteService) => 
            {
                var clientes = await clienteService.ListarClientesAsync();

                if (clientes is not null)
                    return Results.Ok(clientes);

                return Results.NotFound();
            });
        }

        public static void CadastrarClienteAsync(this WebApplication app)
        {
            app.MapPost(_rota , [AllowAnonymous] async (
                ClienteModelCriar clienteModel,
                IClienteService clienteService,
                IValidator<ClienteModelCriar> validacao
                ) =>
            {
                var resultado = await validacao.ValidateAsync(clienteModel);

                    if (!resultado.IsValid)
                        return Results.ValidationProblem(resultado.ToDictionary());

                var sucesso = await clienteService.CadastrarAsync(clienteModel);

                return sucesso ? Results.Ok() : Results.BadRequest(new MensagemRetornoModel(400, "Falha ao cadastrar cliente."));
            });
        }
    }
}
