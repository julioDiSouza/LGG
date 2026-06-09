using LGG.Dominio.Library.Entidades;

namespace LGG.Dominio.Library.Interfaces.IRepositorios
{
    public interface IVideoEstatisticaRepositorio
    {
        Task<VideoEstatistica> AdicionarAsync(VideoEstatistica video);
        Task<List<VideoEstatistica>> ObterTopDezAsync(long videoCicloId);
    }
}
