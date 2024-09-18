using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.UseCases.Library.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> CadastrarAsync(UsuarioModelCriar usuarioDto);
        Task<ApplicationUser?> ConsultarPorEmailAsync(string email);
        Task<ApplicationUser?> ConsultarPorResetTokenAsync(string resetToken);
        Task AtualizarAsync(ApplicationUser usuario);
        Task AtualizarSenhaAsync(ApplicationUser usuario, string senha);
    }
}