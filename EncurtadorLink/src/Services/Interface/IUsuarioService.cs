using EncurtadorLink.Model.Usuario;

namespace EncurtadorLink.Services.Interface
{
    public interface IUsuarioService
    {
        Task<bool> CadastrarUsuario(UsuarioDtoCriar usuarioDto);
    }
}
