using LGG.Global.Library.Enumeradores;
using LGG.Aplicacao.Library.Dtos.Youtube;

namespace LGG.Aplicacao.Library.Interfaces.IYoutube
{
    public interface IYoutubeService
    {
        Task<SearchListResponseDto?> ObterCanalPorNomeAsync(string nome);
        Task<ChannelListResponseDto?> ObterCanalPorIdAsync(string id);
        Task AtualizarStatusAsync(string usuarioId, CanalStatusEnum status);
    }
}
