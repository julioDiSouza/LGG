using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.UseCases.Library.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> CadastrarAsync(UsuarioModelCriar usuarioDto);
    }
}
