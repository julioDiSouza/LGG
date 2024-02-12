using EncurtadorLink.Services.Library.Models;

namespace EncurtadorLink.Services.Library.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> CadastrarAsync(UsuarioModelCriar usuarioCriar);
    }
}
