using EncurtadorLink.Entidades;
using EncurtadorLink.Model.Requisicao;
using EncurtadorLink.Services.Interface;

namespace EncurtadorLink.Rotas
{
    public static class RaizRota
    {
        private static string _rota = string.Empty;
        public static void Raiz(this WebApplication app, string rota)
        {
            _rota = rota;
            app.EncurtarLink();
            app.RecuperarLink();
        }

        public static void EncurtarLink(this WebApplication app)
        {
            app.MapPost(_rota + "/encurtar", async (
                UrlRequisicaoModel requisicao,
                IUrlEncurtadorService urlEncurtadorService,
                HttpContext httpContext
                ) =>
            {
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

        public static void RecuperarLink(this WebApplication app)
        {
            app.MapGet(_rota + "/{codigo}", async (string codigo, IUrlEncurtadorService urlEncurtadorService) => {
                var urlEncurtada = await urlEncurtadorService.RecuperarLinkAsync(codigo);

                if (urlEncurtada is null)
                    return Results.NotFound();

                return Results.Redirect(urlEncurtada.UrlLonga);
            });
        }
    }
}
