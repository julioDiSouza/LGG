using LGG.Dominio.Library.Entidades;

namespace LGG.Dominio.Library.Interfaces.IRepositorios.IVideo
{
    public interface IVideoRepositorio
    {
        Task<Video> AdicionarAsync(Video video);
        Task<Video> ObterPorIdAsync(long id);
        Task<List<Video>> ObterTodosAsync();
        Task<Video?> ObterPorUsuarioIdCampIdAsync(string usuarioId, int id);
        Task<Video> AtualizarAsync(long id, string titulo, string descricao, string url);
        Task<Video> ApagarPorIdAsync(long id);
        Task RemoverDefinitivoPorIdAsync(long id);
    }
}
