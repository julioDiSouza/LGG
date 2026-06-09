using LGG.Dominio.Library.Entidades;

namespace LGG.Dominio.Library.Interfaces.IRepositorios
{
    public interface IVideoBaldeRepositorio
    {
        Task<IEnumerable<long>> ObterVideosIdNoBaldeAsync(long cicloId);
        Task AdicionarAsync(List<VideoBalde> videosBalde);
        Task<IEnumerable<VideoBalde>> ObterVideosBaldeAsync(long cicloId);
        Task<bool> MetricasFinalizadasAsync(long cicloId);
    }
}
