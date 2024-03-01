using System.Net.Http.Headers;
using EncurtadorLink.Robot.Service.Services.Interfaces;
using EncurtadorLink.Shared.Library.Services.Interfaces;

namespace EncurtadorLink.Robot.Service.Services
{
    public class Monitoramento : IMonitoramento
    {
        private readonly HttpClient _httpClient;
        private readonly IProvedorTokenAutenticacao _provedorToken;

        public Monitoramento(IProvedorTokenAutenticacao provedorToken, HttpClient httpClient)
        {
            _provedorToken = provedorToken;
            _httpClient = httpClient;
        }

        public async Task TestarAutenticacaoAsync()
        {
            var urlTeste = "https://localhost:7181/api/teste/autenticacao";

            var requisicao = new HttpRequestMessage(HttpMethod.Get, urlTeste);
            requisicao.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _provedorToken.ObterTokenString());
            requisicao.Headers.Add("Accept", "application/json");

            var response = await _httpClient.SendAsync(requisicao);
            response.EnsureSuccessStatusCode();

            var dados = await response.Content.ReadAsStringAsync();

            Console.WriteLine(dados);
        }
    }
}
