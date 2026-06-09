using LGG.Dominio.Library.Entidades;

namespace LGG.Aplicacao.Library.Interfaces.IServices
{
    public interface IVideoMetricasService
    {
        Task<CampeonatoMensal?> ObterCampeonatoVigenteAsync();
        Task<IEnumerable<VideoBalde?>> ObterVideosBaldeAsync();
        Task AdicionarVideoEstatisticaAsync(VideoEstatistica videoEstatistica);
    }
}
