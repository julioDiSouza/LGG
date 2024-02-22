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
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var model = JsonConvert.DeserializeObject<RequisicaoUrlModel>(requestBody);

            var resultado = await _validacao.ValidateAsync(model!);

            if (!resultado.IsValid)
                return new BadRequestObjectResult(resultado.ToDictionary());

            if (!Uri.TryCreate(model!.Url, UriKind.Absolute, out _))
                return new BadRequestObjectResult("URL é inválida");

            var enderecoCurto = _configuration["EnderecoCurto"]! + "/EO5si";

            return new OkObjectResult(new { Url = enderecoCurto });
        }
    }
}
