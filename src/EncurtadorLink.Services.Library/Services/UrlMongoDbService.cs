using AutoMapper;
using EncurtadorLink.Services.Library.Models;
using EncurtadorLink.Services.Library.Services.Interfaces;
using EncurtadorLink.Services.Library.UnidadeTrabalho.Interfaces;

namespace EncurtadorLink.Services.Library.Services
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

        public async Task<UrlMongoDbModel> ConsultarPorCodigoAsync(string codigo)
        {
             var entidade = await _udt.UrlMongoDBRepositorio.ConsultarPrimeiroAsync(x => 
             x.Codigo == codigo && 
             x.Ativo == true);

            return entidade is not null ? _mapper.Map<UrlMongoDbModel>(entidade) : null;
        }
    }
}
