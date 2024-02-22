using Newtonsoft.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.CriaLink.Function
{
    public class CriaLink
    {
        private readonly IConfiguration _configuration;
        private readonly IValidator<RequisicaoUrlModel> _validacao;
        private readonly ILogger<CriaLink> _logger;
        private readonly IUrlService _urlService;

        public CriaLink(IConfiguration configuration, IUrlService urlService, ILogger<CriaLink> logger, IValidator<RequisicaoUrlModel> validacao)
        {
            _configuration = configuration;
            _urlService = urlService;
            _logger = logger;
            _validacao = validacao;
        }

        [Function("CriaLink")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var reqModel = JsonConvert.DeserializeObject<RequisicaoUrlModel>(requestBody);

                var resultado = await _validacao.ValidateAsync(reqModel!);

                if (!resultado.IsValid)
                    return new BadRequestObjectResult(resultado.ToDictionary());

                if (!Uri.TryCreate(reqModel!.Url, UriKind.Absolute, out _))
                    return new BadRequestObjectResult("URL é inválida");

                var urlSqlModel = new UrlSqlModel
                {
                    Id = Guid.NewGuid(),
                    UrlLonga = reqModel.Url,
                    UrlCurta = _configuration["EnderecoCurto"]!,
                    IP = req.HttpContext.Connection.RemoteIpAddress?.ToString()!,
                    DataInclusao = DateTime.Now
                };

                urlSqlModel = await _urlService.GerarUrlCodigoUnicoAsync(urlSqlModel);

                return new OkObjectResult(new { UrlCurta = urlSqlModel.UrlCurta });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
