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

        public async Task<UrlMongoDbModel> ConsultarPorCodigoAsync(string codigo)
        {
            var entidade = _udt.UrlMongoDBRepositorio.ConsultarPrimeiro(x =>
            x.Codigo == codigo &&
            x.Ativo == true);

            await Task.Run(() => { });

            return entidade is not null ? _mapper.Map<UrlMongoDbModel>(entidade) : null;
        }
    }
}
