using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;

namespace EncurtadorLink.UseCases.Library.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioService(IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> CadastrarAsync(UsuarioModelCriar usuarioCriar)
        {
            var usuarioIdentity = _mapper.Map<ApplicationUser>(usuarioCriar);
            var resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity, usuarioCriar.Senha);

            return resultadoIdentity.Succeeded;
        }

        public async Task<ApplicationUser?> ConsultarPorEmailAsync(string email) =>
            await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<ApplicationUser?> ConsultarPorResetTokenAsync(string resetToken) =>
            await _userManager.Users.FirstOrDefaultAsync(x => x.ResetPasswordToken == resetToken);

        public async Task AtualizarAsync(ApplicationUser usuario) =>
            await _userManager.UpdateAsync(usuario);

        public async Task AtualizarSenhaAsync(ApplicationUser usuario, string senha)
        {
            usuario.PasswordHash = _userManager.PasswordHasher.HashPassword(usuario, senha);
            await _userManager.UpdateAsync(usuario);
        }
    }
}