using AutoMapper;
using EncurtadorLink.Entidades;
using EncurtadorLink.Model.Usuario;
using Microsoft.AspNetCore.Identity;
using EncurtadorLink.Services.Interface;
using EncurtadorLink.Infra.BancoDados.SQL;

namespace EncurtadorLink.Services
{
    public class ClienteService : IClienteService
    {
        private IMapper _mapper;
        private readonly ContextoDb _dbContexto;
        private UserManager<ApplicationUser> _userManager;

        public ClienteService(ContextoDb dbContexto, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _dbContexto = dbContexto;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> CadastrarAsync(ClienteDtoCriar clienteDtoCriar)
        {
            var dataAgora = DateTime.Now;

            var cliente = _mapper.Map<Cliente>(clienteDtoCriar);
                cliente!.Id = Guid.NewGuid();
                cliente.Ativo = true;
                cliente.DataInclusao = dataAgora;

            var usuarioIdentity = _mapper.Map<ApplicationUser>(clienteDtoCriar);
                usuarioIdentity!.ClienteId = cliente.Id;
                usuarioIdentity.Ativo = true;
                usuarioIdentity.DataInclusao = dataAgora;

            await _dbContexto.Cliente.AddAsync(cliente);
            await _dbContexto.SaveChangesAsync();

            var resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity!, clienteDtoCriar.Senha);

            return resultadoIdentity.Succeeded;
        }
    }
}
