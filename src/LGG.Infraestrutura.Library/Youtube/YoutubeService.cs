using LGG.Global.Library.Extensoes;
using LGG.Global.Library.Enumeradores;
using Microsoft.Extensions.Configuration;
using LGG.Aplicacao.Library.Dtos.Youtube;
using LGG.Aplicacao.Library.Interfaces.IHttp;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Interfaces.IYoutube;

namespace LGG.Infraestrutura.Library.Youtube
{
    public class YoutubeService : IYoutubeService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpService _httpService;
        private readonly IUnidTrabalho _ut;
        private readonly string _apiKey;
        private string _apiUrl;

        public YoutubeService(IConfiguration configuration, IHttpService httpService, IUnidTrabalho ut)
        {
            _configuration = configuration;
            _httpService = httpService;
            _ut = ut;
            _apiKey = _configuration["Youtube:ChaveApi"]!.ToString();
            _apiUrl = _configuration["Youtube:EnderecoApi"]!.ToString().AjustarUrl();
        }

        public async Task<SearchListResponseDto?> ObterCanalPorNomeAsync(string nome)
        {
            var url = $"{_apiUrl}search?part=snippet&q={nome.RemoverArroba()}&type=channel&key={_apiKey}";

            var resultado = await _httpService.GetAsync<SearchListResponseDto>(url);

            return resultado;
        }

        public async Task<ChannelListResponseDto?> ObterCanalPorIdAsync(string id)
        {
            var url = $"{_apiUrl}channels?part=snippet,statistics&id={id}&key={_apiKey}";

            var resultado = await _httpService.GetAsync<ChannelListResponseDto>(url);

            return resultado;
        }

        public async Task AtualizarStatusAsync(string usuarioId, CanalStatusEnum status)
        {
            await _ut.CanalRepositorio.AtualizarStatusAsync(new Guid(usuarioId), status);
        }

        public async Task AtualizarInformacoesAsync(string usuarioId, string canalId, string nome, 
            string titulo, string urlPersonalizada, string urlImagem)
        {
            await _ut.CanalRepositorio
                .AtualizarInformacoesAsync(new Guid(usuarioId), canalId, nome, titulo, urlPersonalizada, urlImagem);
        }
    }
}
