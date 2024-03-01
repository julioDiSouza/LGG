using Polly;
using System.Net;
using Polly.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using EncurtadorLink.Shared.Library.Services.Interfaces;

namespace EncurtadorLink.Robot.Service.Extensao
{
    public static class TokenAutenticacaoExtensao
    {
        public static IAsyncPolicy<HttpResponseMessage> ConfiguracaoTokenPolicy(this IServiceCollection services)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(response => response.StatusCode == HttpStatusCode.Unauthorized)
                .WaitAndRetryAsync(3, tentativa => TimeSpan.FromSeconds(Math.Pow(2, tentativa)), async (response, timeSpan) =>
                {
                    if (response.Result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var provedorToken = services.BuildServiceProvider().GetRequiredService<IProvedorTokenAutenticacao>();
                        await provedorToken.AtualizarTokenAsync(response.Result);
                    }
                });          
        }
    }
}
