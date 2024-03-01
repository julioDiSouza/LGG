using AutoMapper;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;

namespace EncurtadorLink.UseCases.Library.Services
{
    public class UrlMongoDbService : IUrlMongoDbService
    {
        private readonly IUnidadeTrabalho _udt;
        private readonly IMapper _mapper;

        public UrlMongoDbService(IUnidadeTrabalho udt, IMapper mapper)
        {
            _udt = udt;
            _mapper = mapper;
        }

        public async Task<UrlMongoDbModel?> ConsultarPorCodigoAsync(string codigo)
        {
            var entidade = await _udt.UrlMongoDbRepositorio.ConsultarPorCodigoAsync(codigo);

            return _mapper.Map<UrlMongoDbModel>(entidade) ?? null;
        }
    }
}
