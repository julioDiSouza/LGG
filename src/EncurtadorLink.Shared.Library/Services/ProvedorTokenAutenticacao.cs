using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.Shared.Library.Services.Interfaces;
using EncurtadorLink.Shared.Library.Models.Configuracoes;

namespace EncurtadorLink.Shared.Library.Services
{
    public class ProvedorTokenAutenticacao : IProvedorTokenAutenticacao
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConfiguracaoAutenticacao _configuracaoAutenticacao;

        public TokenModel _tokenModel;

        public ProvedorTokenAutenticacao(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _configuracaoAutenticacao = configuration.GetSection("ConfiguracaoAutenticacao").Get<ConfiguracaoAutenticacao>()!;
            _tokenModel = new TokenModel();
        }

        public async Task AtualizarTokenAsync(HttpResponseMessage response = null)
        {
            await ObterNovoTokenAsync();

            if (response != null) 
            {
                response.RequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenModel.AccessToken);
            }
        }

        public TokenModel ObterToken()
        {
            return _tokenModel;
        }

        public async Task<string?> ObterTokenString()
        {
            await AtualizarTokenAsync();
            return _tokenModel?.AccessToken;
        }

        private async Task ObterNovoTokenAsync()
        {
            var requisicao = new HttpRequestMessage(HttpMethod.Get, _configuracaoAutenticacao.UrlToken);

            requisicao.Headers.Add("client_id", _configuracaoAutenticacao.ClientId);
            requisicao.Headers.Add("client_secret", _configuracaoAutenticacao.ClientSecret);

            var response = await _httpClientFactory.CreateClient().SendAsync(requisicao);
            response.EnsureSuccessStatusCode();

            _tokenModel = JsonConvert.DeserializeObject<TokenModel>(await response.Content.ReadAsStringAsync())!;
        }
    }
}
