using FluentValidation;
using EncurtadorLink.Filtros;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.Rotas
{
    public static class UrlCurtaRota
    {
        private static string _rota = string.Empty;
        private static IConfiguration _configuration;

        public static void EncurtaUrl(this WebApplication app, string rota, IConfiguration configuration)
        {
            _rota = rota;
            _configuration = configuration;
            app.CriaUrlCurtaAsync();
            app.CriaUrlCurtaPublicoAsync();
            app.ObterUrlAsync();
            app.ApagarUrlAsync();
        }

        public static void CriaUrlCurtaAsync(this WebApplication app)
        {
            app.MapPost(_rota + "/encurta-url/logado", [Authorize] async (
                UrlModelRequisicao requisicao,
                IUrlService urlService,
                HttpContext httpContext,
                IValidator<UrlModelRequisicao> validacao
                ) =>
            {
                return await ProcessarUrlAsync(app, requisicao, urlService, httpContext, validacao, ObterId(httpContext));
            });
        }

        public static void CriaUrlCurtaPublicoAsync(this WebApplication app)
        {
            app.MapPost(_rota + "/encurta-url", async (
                UrlModelRequisicao requisicao,
                IUrlService urlService,
                HttpContext httpContext,
                IValidator<UrlModelRequisicao> validacao,
                IConfiguration configuration
                ) =>
            {
                return await ProcessarUrlAsync(app, requisicao, urlService, httpContext, validacao);
            }).AddEndpointFilter(new AllowAnonymousTempToken(_configuration));
        }

        public static void ObterUrlAsync(this WebApplication app)
        {
            app.MapGet(_rota + "/urls/{pagina}", [Authorize] async (
                int pagina,
                IUrlService urlService,
                HttpContext httpContext,
                IValidator<UrlModelRequisicao> validacao
                ) =>
            {
                return Results.Ok(await urlService.ObterUrlsPaginadasAsync(ObterId(httpContext), pagina));
            });
        }

        public static void ApagarUrlAsync(this WebApplication app)
        {
            app.MapDelete(_rota + "/url/{id}", [Authorize] async (
                Guid id,
                IUrlService urlService,
                HttpContext httpContext,
                IValidator<UrlModelRequisicao> validacao
                ) =>
            {
                await urlService.ApagarUrlAsync(ObterId(httpContext), id);

                return Results.Ok();
            });
        }

        #region ..:: Privados ::..

        private static async Task<IResult> ProcessarUrlAsync(
            WebApplication app,
            UrlModelRequisicao requisicao,
            IUrlService urlService,
            HttpContext httpContext,
            IValidator<UrlModelRequisicao> validacao, 
            Guid? id = null)
        {
            var resultado = await validacao.ValidateAsync(requisicao);
            if (!resultado.IsValid)
                return Results.ValidationProblem(resultado.ToDictionary());

            if (!Uri.TryCreate(requisicao.UrlLonga, UriKind.Absolute, out _))
                return Results.BadRequest(new MensagemRetornoModel(400, "URL é inválida."));

            var enderecoCurto = app.Configuration["EnderecoCurto"]!;


            var urlSql = new UrlSqlModel
            {
                Id = Guid.NewGuid(),
                ClienteId = id,
                UrlLonga = requisicao.UrlLonga,
                UrlCurta = enderecoCurto,
                IP = httpContext.Connection.RemoteIpAddress?.ToString()!,
                DataInclusao = DateTime.Now
            };

            urlSql = await urlService.GerarUrlCodigoUnicoAsync(urlSql);
            return Results.Ok(new { UrlCurta = urlSql.UrlCurta });
        }

        private static Guid ObterId(HttpContext httpContext)
        {
            var tokenJwt = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ","");
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenJwt);

            return Guid.Parse(token.Claims.FirstOrDefault(x => x.Type == "id")!.Value);
        }

        #endregion ..:: Fim Privados ::..

    }
}
