using LGG.Dominio.Library.Entidades;

namespace LGG.Dominio.Library.Interfaces.IRepositorios.IVideoEstatistica
{
    public interface IVideoEstatisticaRepositorio
    {
        Task<VideoEstatistica> AdicionarAsync(VideoEstatistica video);
    }
}
