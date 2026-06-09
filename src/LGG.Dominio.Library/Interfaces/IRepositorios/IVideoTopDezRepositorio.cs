using LGG.Dominio.Library.Entidades;

namespace LGG.Dominio.Library.Interfaces.IRepositorios
{
    public interface IVideoTopDezRepositorio
    {
        Task<VideoTopDez> AdicionarAsync(VideoTopDez videoTopDez);
    }
}
