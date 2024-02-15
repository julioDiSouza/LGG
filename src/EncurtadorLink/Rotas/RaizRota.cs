using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.Rotas
{
    public static class RaizRota
    {
        private static string _rota = string.Empty;
        public static void Raiz(this WebApplication app, string rota)
        {
            _rota = rota;
            app.CriaLink();
            app.RecuperaLink();
            app.Login();
            app.Logout();
        }

        public static void CriaLink(this WebApplication app)
        {
            app.MapPost(_rota + "/link", [Authorize] async (
                RequisicaoUrlModel requisicao,
                IUrlService urlService,
                HttpContext httpContext,
                IValidator<RequisicaoUrlModel> validacao
                ) =>
            {
                var resultado = await validacao.ValidateAsync(requisicao);

                if (!resultado.IsValid)
                    return Results.ValidationProblem(resultado.ToDictionary());

                if (!Uri.TryCreate(requisicao.Url, UriKind.Absolute, out _))
                    return Results.BadRequest("URL é inválida");

                var enderecoCurto = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/";

                var urlSql = new UrlSql
                {
                    Id = Guid.NewGuid(),
                    UrlLonga = requisicao.Url,
                    UrlCurta = enderecoCurto,
                    IP = httpContext.Connection.RemoteIpAddress?.ToString()!,
                    DataInclusao = DateTime.Now 
                };

                urlSql = await urlService.GerarUrlCodigoUnicoAsync(urlSql);

                return Results.Ok(urlSql.UrlCurta);
            });
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
                RequisicaoLoginModel requisicao,
                ITokenService tokenService,
                HttpContext httpContext,
                IValidator<RequisicaoLoginModel> validacao
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
