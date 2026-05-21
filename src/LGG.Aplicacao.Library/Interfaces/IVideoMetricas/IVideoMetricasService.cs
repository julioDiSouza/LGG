using LGG.Dominio.Library.Entidades;

namespace LGG.Aplicacao.Library.Interfaces.IVideoMetricas
{
    public interface IVideoMetricasService
    {
        Task<CampeonatoMensal?> ObterCampeonatoVigenteAsync();
        Task<IEnumerable<VideoBalde?>> ObterVideosBaldeAsync();
    }
}
