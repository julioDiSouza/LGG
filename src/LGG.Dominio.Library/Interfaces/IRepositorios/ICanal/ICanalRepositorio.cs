using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;

namespace LGG.Dominio.Library.Interfaces.IRepositorios.ICanal
{
    public interface ICanalRepositorio
    {
        Task<Canal> AdicionarAsync(Canal canal);
        Task<bool> CodigoLiberadoAsync(string codigo);
        Task AtualizarStatusAsync(Guid usuarioId, CanalStatusEnum status);
        Task RemoverDefinitivoPorIdAsync(Guid id);
    }
}