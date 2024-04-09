using FluentValidation;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.Shared.Library.Services.Interfaces;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.Rotas
{
    public static class UrlCurtaRota
    {
        private static string _rota = string.Empty;

        public static void EncurtaUrl(this WebApplication app, string rota)
        {
            _rota = rota;
            app.CriaUrlCurtaAsync();
            app.CriaUrlCurtaPublicoAsync();
        }

        public static void CriaUrlCurtaAsync(this WebApplication app)
        {
            app.MapPost(_rota , [Authorize] async (
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
            app.MapPost(_rota + "/publico", [AllowAnonymous] async (
                UrlModelRequisicao requisicao,
                IUrlService urlService,
                HttpContext httpContext,
                IValidator<UrlModelRequisicao> validacao,
                ICriptografador criptografador
                ) =>
            {
                var tokenValido = criptografador.TokenTemporarioValido(httpContext.Request.Query["temp_token"]!);

                if (!tokenValido)
                    return Results.BadRequest(new MensagemRetornoModel(400, "Token inválido."));

                return await ProcessarUrlAsync(app, requisicao, urlService, httpContext, validacao);
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
