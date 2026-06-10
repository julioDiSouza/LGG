using LGG.Dominio.Library.Entidades;

namespace LGG.Dominio.Library.Interfaces.IRepositorios
{
    public interface IVideoTopDezRepositorio
    {
        Task<List<VideoTopDez>> ObterPorCampeonatoMensalIdAsync(int campeonatoMensalId, long videoCicloId);
        Task AdicionarAsync(VideoTopDez videoTopDez);
    }
}
