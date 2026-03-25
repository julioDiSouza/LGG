using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;

namespace LGG.Dominio.Library.Interfaces.IRepositorios.IUsuario
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> AdicionarAsync(Usuario usuario);
        Task<Usuario?> ObterPorIdAsync(Guid id);
        Task<Usuario?> ObterPorEmailAsync(string email);
        Task<Usuario?> ObterPorEmailSenhaAsync(string email, string senha);
        Task<List<Usuario>> ObterTodosAsync();
        Task<Usuario> AtualizarAsync(Guid id, string nome, UsuarioSexoEnum sexo, DateTime dataNascimento);
        Task<Usuario> AtualizarStatusAsync(Guid id, UsuarioStatusEnum status);
        Task<Usuario?> ApagarPorIdAsync(Guid id);
        Task RemoverDefinitivoPorIdAsync(Guid id);
    }
}
