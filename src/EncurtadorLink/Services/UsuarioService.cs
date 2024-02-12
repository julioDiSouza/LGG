using AutoMapper;
using EncurtadorLink.Entidades;
using EncurtadorLink.Model.Usuario;
using Microsoft.AspNetCore.Identity;
using EncurtadorLink.Services.Interface;

namespace EncurtadorLink.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;

        public UsuarioService(IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> CadastrarAsync(UsuarioDtoCriar usuarioDto)
        {
            var usuarioIdentity = _mapper.Map<ApplicationUser>(usuarioDto);
            var resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity, usuarioDto.Senha);

            return resultadoIdentity.Succeeded;
        }
    }
}
