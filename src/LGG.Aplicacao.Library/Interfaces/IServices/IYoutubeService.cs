using LGG.Aplicacao.Library.Dtos.Youtube;

namespace LGG.Aplicacao.Library.Interfaces.IServices
{
    public interface IYoutubeService
    {
        Task<SearchListResponseDto?> ObterCanalPorNomeAsync(string nome);
        Task<ChannelListResponseDto?> ObterCanalPorIdAsync(string id);
        Task<VideoListResponseDto?> ObterVideosPorIdAsync(string id);
    }
}
