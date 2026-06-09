using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;

namespace LGG.Dominio.Library.Interfaces.IRepositorios
{
    public interface IVideoCicloRepositorio
    {
        Task<VideoCiclo?> ObterUltimoVideoCicloAsync();
        Task AdicionarAsync(VideoCiclo videoCiclo);
        Task AtualizarStatusAsync(long videoCicloId, int campeonatoMensalId, VideoCicloStatusEnum status);
    }
}
