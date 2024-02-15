using AutoMapper;
using Microsoft.AspNetCore.Identity;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;

namespace EncurtadorLink.UseCases.Library.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IUnidadeTrabalho _udt;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClienteService(IUnidadeTrabalho udt, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _udt = udt;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<ClienteModel>?> ListarClientesAsync()
        {
            var entidades = await _udt.ClienteRepositorio.ListarClientesAsync();

            return _mapper.Map<List<ClienteModel>>(entidades) ?? null;
        }

        public async Task<bool> CadastrarAsync(ClienteModelCriar clienteCriar)
        {
            var dataAgora = DateTime.Now;

            var cliente = _mapper.Map<Cliente>(clienteCriar);
            cliente!.Id = Guid.NewGuid();
            cliente.Ativo = true;
            cliente.DataInclusao = dataAgora;

            var usuarioIdentity = _mapper.Map<ApplicationUser>(clienteCriar);
            usuarioIdentity!.ClienteId = cliente.Id;
            usuarioIdentity.Ativo = true;
            usuarioIdentity.DataInclusao = dataAgora;

            await _udt.ClienteRepositorio.AdicionarAsync(cliente);

            var resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity!, clienteCriar.Senha);

            return resultadoIdentity.Succeeded;
        }
    }
}
