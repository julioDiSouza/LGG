using FluentValidation;
using EncurtadorLink.Filtros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.Rotas
{
    public static class ClienteRota
    {
        private static string _rota = string.Empty;
        private static IConfiguration _configuration;

        public static void Cliente(this WebApplication app, string rota, IConfiguration configuration)
        {
            _rota = rota;
            _configuration = configuration;
            app.ListarClientesAsync();
            app.CadastrarClienteAsync();
            app.ValidarClienteAsync();
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

                var sucesso = await clienteService.CadastroViaWebAsync(clienteModel);

                return sucesso ? Results.Ok() : Results.BadRequest(new MensagemRetornoModel(400, "Falha ao cadastrar cliente."));
            });
        }

        public static void ValidarClienteAsync(this WebApplication app)
        {
            app.MapPost(_rota + "/validacao", async (
                ClienteModelCodigo cliente,
                IClienteService clienteService 
                ) =>
            {
                var resultado = await clienteService.ValidarClienteAsync(cliente);

                return resultado ? Results.Ok() : Results.BadRequest();
            }).AddEndpointFilter(new AllowAnonymousTempToken(_configuration));
        }
    }
}
