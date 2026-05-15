using LGG.Dominio.Library.Entidades;

namespace LGG.Dominio.Library.Interfaces.IRepositorios.IVideoCiclo
{
    public interface IVideoCicloRepositorio
    {
        Task<VideoCiclo?> ObterUltimoVideoCicloAsync();
        Task AdicionarAsync(VideoCiclo videoCiclo);
    }
}
