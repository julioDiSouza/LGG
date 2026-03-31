using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;

namespace LGG.Dominio.Library.Interfaces.IRepositorios.ICanal
{
    public interface ICanalRepositorio
    {
        Task<Canal?> ObterPorIdAsync(Guid canalId);
        Task<Canal?> ObterPorCanalIdAsync(string canalId);
        Task<Canal> AdicionarAsync(Canal canal);
        Task<bool> CodigoLiberadoAsync(string codigo);
        Task AtualizarStatusAsync(Guid usuarioId, CanalStatusEnum status);
        Task AtualizarInformacoesAsync(Guid usuarioId, string canalId, string nome, string titulo, string urlPersonalizada, string urlImagem);
        Task RemoverDefinitivoPorIdAsync(Guid id);
    }
}