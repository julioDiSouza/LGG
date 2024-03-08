using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.Rotas
{
    public static class UrlCurtaRota
    {
        private static string _rota = string.Empty;

        public static void EncurtaUrl(this WebApplication app, string rota)
        {
            _rota = rota;
            app.CriaUrlCurta();
        }

        public static void CriaUrlCurta(this WebApplication app)
        {
            app.MapPost(_rota , [Authorize] async (
                UrlModelRequisicao requisicao,
                IUrlService urlService,
                HttpContext httpContext,
                IValidator<UrlModelRequisicao> validacao
                ) =>
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
                    UrlLonga = requisicao.UrlLonga,
                    UrlCurta = enderecoCurto,
                    IP = httpContext.Connection.RemoteIpAddress?.ToString()!,
                    DataInclusao = DateTime.Now
                };
                urlSql = await urlService.GerarUrlCodigoUnicoAsync(urlSql);
                return Results.Ok(new { UrlCurta = urlSql.UrlCurta });
            });
        }
    }
}
