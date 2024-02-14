using AutoMapper;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;

namespace EncurtadorLink.UseCases.Library.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IUnidadeTrabalho _udt;
        private readonly IMapper _mapper;

        public ClienteService(IUnidadeTrabalho udt, IMapper mapper)
        {
            _udt = udt;
            _mapper = mapper;
        }

        public async Task<List<ClienteModel>?> ListarClientesAsync()
        {
            var entidades = await _udt.ClienteRepositorio.ListarClientesAsync();

            return _mapper.Map<List<ClienteModel>>(entidades) ?? null;
        }
    }
}
