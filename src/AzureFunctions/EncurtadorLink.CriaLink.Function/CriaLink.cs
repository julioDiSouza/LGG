using System.Text;
using Newtonsoft.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.CriaLink.Function
{
    public class CriaLink
    {
        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly IValidator<RequisicaoUrlModel> _validacao;
        private readonly ILogger<CriaLink> _logger;
        private readonly IUrlService _urlService;

        public CriaLink(IConfiguration configuration, IUrlService urlService, ILogger<CriaLink> logger, IValidator<RequisicaoUrlModel> validacao)
        {
            _configuration = configuration;
            _urlService = urlService;
            _logger = logger;
            _validacao = validacao;
            _tokenHandler = new JwtSecurityTokenHandler();
            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuers = _configuration.GetSection("TokenJWT:Issuers").Get<List<string>>(),
                ValidateAudience = true,
                ValidAudiences = _configuration.GetSection("TokenJWT:Audiences").Get<List<string>>(),
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["TokenJWT:ChaveInterna"]!.ToString()))
            };
        }

        [Function("cria-link-curto")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            try
            {
                if (!ValidarToken(req))
                    return new StatusCodeResult(StatusCodes.Status401Unauthorized);

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

        #region ..:: Privados ::..

        private bool ValidarToken(HttpRequest httpRequest)
        {
            try
            {
                var token = httpRequest.Headers["Authorization"].ToString().Split(' ')[1];
                var principal = _tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);

                //// Access claims like this:
                //var claims = new ClaimsIdentity(((JwtSecurityToken)validatedToken).Claims);
                //var userId = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return principal.Identity!.IsAuthenticated;
            }
            catch (Exception) { }

            return false;
        }

        #endregion ..:: Fim Privados ::..
    }
}
