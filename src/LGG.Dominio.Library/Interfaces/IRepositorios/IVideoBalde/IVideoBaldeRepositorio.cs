using LGG.Dominio.Library.Entidades;

namespace LGG.Dominio.Library.Interfaces.IRepositorios.IVideoBalde
{
    public interface IVideoBaldeRepositorio
    {
        Task<IEnumerable<long>> ObterVideosIdNoBaldeAsync(long cicloId);
        Task AdicionarAsync(List<VideoBalde> videosBalde);
    }
}
