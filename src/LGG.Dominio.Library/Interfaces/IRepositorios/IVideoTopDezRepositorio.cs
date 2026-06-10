using LGG.Dominio.Library.Entidades;

namespace LGG.Dominio.Library.Interfaces.IRepositorios
{
    public interface IVideoTopDezRepositorio
    {
        Task<List<VideoTopDez>> ObterAsync();
        Task AdicionarAsync(VideoTopDez videoTopDez);
    }
}
