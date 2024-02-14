using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using EncurtadorLink.Model.Requisicao;
using EncurtadorLink.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.Rotas
{
    public static class RaizRota
    {
        private static string _rota = string.Empty;
        public static void Raiz(this WebApplication app, string rota)
        {
            _rota = rota;
            //app.CriaLink();
            app.RecuperaLink();
            //app.Login();
            //app.Logout();
        }

        /*
        public static void CriaLink(this WebApplication app)
        {
            app.MapPost(_rota + "/link", [Authorize] async (
                UrlRequisicaoModel requisicao,
                IUrlEncurtadorService urlEncurtadorService,
                HttpContext httpContext,
                IValidator<UrlRequisicaoModel> validacao
                ) =>
            {
                var resultado = await validacao.ValidateAsync(requisicao);

                if (!resultado.IsValid)
                    return Results.ValidationProblem(resultado.ToDictionary());

                if (!Uri.TryCreate(requisicao.Url, UriKind.Absolute, out _))
                    return Results.BadRequest("URL é inválida");

                var urlEncurtada = new UrlEncurtada
                {
                    Id = Guid.NewGuid(),
                    UrlLonga = requisicao.Url,
                    UrlCurta = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/",
                    DataInclusaoUtc = DateTime.Now 
                };

                urlEncurtada = await urlEncurtadorService.GerarCodigoUnicoAsync(urlEncurtada);

                return Results.Ok(urlEncurtada.UrlCurta);
            });
        }
        */

        public static void CriaLink(this WebApplication app)
        {
            /*
            app.MapPost(_rota + "/link", [Authorize] async (
                UrlRequisicaoModel requisicao,
                IUrlMongoDbService urlMongoService,
                HttpContext httpContext,
                IValidator<UrlRequisicaoModel> validacao
                ) =>
            {
                var resultado = await validacao.ValidateAsync(requisicao);

                if (!resultado.IsValid)
                    return Results.ValidationProblem(resultado.ToDictionary());

                if (!Uri.TryCreate(requisicao.Url, UriKind.Absolute, out _))
                    return Results.BadRequest("URL é inválida");

                var urlEncurtada = new UrlMongoDbModel
                {
                    Id = Guid.NewGuid().ToString(),
                    UrlLonga = requisicao.Url,
                    Ativo = true
                };

                //$"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/"

                urlEncurtada = await urlEncurtadorService.GerarCodigoUnicoAsync(urlEncurtada);

                return Results.Ok(urlEncurtada.UrlCurta);
            });
            */
        }


        public static void RecuperaLink(this WebApplication app)
        {
            app.MapGet(_rota + "/{codigo}", [AllowAnonymous] async (string codigo, [FromServices] IUrlMongoDbService urlMongoService) => {
                try
                {
                    var urlMongo = await urlMongoService.ConsultarPorCodigoAsync(codigo);

                    if (urlMongo is not null)
                        return Results.Redirect(urlMongo.UrlLonga);
                }
                catch (Exception) { }

                return Results.NotFound();
            });
        }

        public static void Login(this WebApplication app)
        {
            app.MapPost(_rota + "/login", [AllowAnonymous] async (
                LoginRequisicaoModel requisicao,
                ITokenService tokenService,
                HttpContext httpContext,
                IValidator<LoginRequisicaoModel> validacao
                ) =>
            {
                var resultado = await validacao.ValidateAsync(requisicao);

                if (!resultado.IsValid)
                    return Results.ValidationProblem(resultado.ToDictionary());

                var ip = httpContext.Connection.RemoteIpAddress?.ToString()!;

                var (token, sucesso) = await tokenService.ObterTokenLoginAsync(requisicao, ip);

                if (!sucesso)
                    return Results.BadRequest(token);

                return Results.Ok(token);
            });
        }

        public static void Logout(this WebApplication app)
        {
            app.MapGet(_rota + "/logout", [Authorize] async (
                ITokenService tokenService
                ) =>
            {
                await tokenService.LogoutAsync();

                return Results.Ok();
            });
        }
    }
}
