using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using Google.Apis.Auth;
using EncurtadorLink.Shared.Library.Classes;
using Newtonsoft.Json;

namespace EncurtadorLink.Rotas
{
    public static class LoginRota
    {
        private static string _rota = string.Empty;
        private static IConfiguration _configuration;

        public static void Login(this WebApplication app, string rota, IConfiguration configuration)
        {
            _rota = rota;
            _configuration = configuration;
            app.LoginAsync();
            app.LogoutAsync();
            app.LoginGoogleAsync();
            app.LoginFacebook();
        }

        public static void LoginAsync(this WebApplication app)
        {
            app.MapPost(_rota + "/login", [AllowAnonymous] async (
                LoginModelRequisicao requisicao,
                ITokenService tokenService,
                HttpContext httpContext,
                IValidator<LoginModelRequisicao> validacao
                ) =>
            {
                var resultado = await validacao.ValidateAsync(requisicao);

                if (!resultado.IsValid)
                    return Results.ValidationProblem(resultado.ToDictionary());

                var (token, sucesso) = await tokenService.ObterTokenLoginAsync(requisicao);

                if (!sucesso)
                    return Results.BadRequest(token);

                return Results.Ok(token);
            });
        }

        public static void LogoutAsync(this WebApplication app)
        {
            app.MapGet(_rota + "/logout", [AllowAnonymous] async (
                ITokenService tokenService
                ) =>
            {
                await tokenService.LogoutAsync();

                return Results.Ok();
            });
        }

        public static void LoginGoogleAsync(this WebApplication app)
        {
            app.MapPost(_rota + "/login-google", [AllowAnonymous] async (
                [FromBody] string credential, 
                ITokenService tokenService,
                IClienteService clienteService 
                ) =>
            {
                try
                {
                    var settings = new GoogleJsonWebSignature.ValidationSettings()
                    {
                        Audience = new List<string> { _configuration["GoogleClientId"]!.ToString() }
                    };

                    var payload = await GoogleJsonWebSignature.ValidateAsync(credential, settings);

                    var model = new LoginModelGoogle
                    {
                        Nome = payload.Name,
                        Email = payload.Email,
                        Senha = Guid.NewGuid().ToString()
                    };

                    var (token, sucesso) = await tokenService.ObterTokenGoogleAsync(model, clienteService);

                    if (!sucesso)
                        return Results.BadRequest(token);

                    return Results.Ok(token);
                }
                catch (Exception ex)
                {

                    throw;
                }
            });
        }

        public static void LoginFacebook(this WebApplication app)
        {
            app.MapPost(_rota + "/login-facebook", [AllowAnonymous] (
                [FromBody] string credential
                ) =>
            {
                try
                {
                    credential = Criptografador.DecryptString(credential, _configuration["CryptoKey"]!);
                    var model = JsonConvert.DeserializeObject<LoginModelFacebook>(credential);
                    model.Senha = Guid.NewGuid().ToString();

                    return Results.Ok(model);
                }
                catch (Exception ex)
                {

                    throw;
                }
            });
        }

    }
}
