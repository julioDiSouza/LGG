using AutoMapper;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Models;
using EncurtadorLink.Entities.Library.Mongo;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;

namespace EncurtadorLink.UseCases.Library.Services
{
    public class UrlService : IUrlService
    {
        private readonly int _tamanho;
        private readonly string _caracteres;
        private readonly IUnidadeTrabalho _udt;
        private readonly IMapper _mapper;

        public UrlService(IConfiguration configuration, IUnidadeTrabalho udt, IMapper mapper)
        {
            _udt = udt;
            _mapper = mapper;
            _tamanho = int.Parse(configuration["ConfiguracaoCodigoUnico:Tamanho"]!);
            _caracteres = configuration["ConfiguracaoCodigoUnico:Caracteres"]!;
        }

        public async Task<UrlSqlModel> GerarUrlCodigoUnicoAsync(UrlSqlModel urlSqlModel)
        {
            urlSqlModel.Codigo = await ObterCodigoUnicoAsync();
            urlSqlModel.UrlCurta += "/" + urlSqlModel.Codigo;

            var urlSqlEntidade = _mapper.Map<UrlSql>(urlSqlModel);

            await _udt.UrlSqlRepositorio.AdicionarAsync(urlSqlEntidade);

            var urlMongoDb = new UrlMongoDb
            {
                Codigo = urlSqlModel.Codigo,
                UrlLonga = urlSqlModel.UrlLonga,
                Ativo = true
            };

            await _udt.UrlMongoDbRepositorio.AdicionarAsync(urlMongoDb);

            return urlSqlModel;
        }

        public async Task<ResultadoPaginadoModel<UrlSqlModelGrid>> ObterUrlsPaginadasAsync(Guid clienteId, int pagina, int quantidade)
        {
            var (total, urls) = await _udt.UrlSqlRepositorio.ObterUrlsPaginadasAsync(clienteId, pagina, quantidade);

            var resultado = new ResultadoPaginadoModel<UrlSqlModelGrid>();
            resultado.Colecao = _mapper.Map<List<UrlSqlModelGrid>>(urls);
            resultado.Pagina = pagina;
            resultado.Quantidade = quantidade;
            resultado.Total = total;

            return resultado;
        }

        public async Task ApagarUrlAsync(Guid clienteId, Guid urlId)
        {
            var urlSql = await _udt.UrlSqlRepositorio.ApagarUrlAsync(clienteId, urlId);

            await _udt.UrlMongoDbRepositorio.ApagarAsync(urlSql.Codigo);
        }

        #region ..:: Privados ::..

        private async Task<string> ObterCodigoUnicoAsync()
        {
            Random random = new();
            var arrCodigo = new char[_tamanho];

            while (true)
            {
                for (int i = 0; i < _tamanho; i++)
                {
                    var indexAleatorio = random.Next(_caracteres.Length);

                    arrCodigo[i] = _caracteres[indexAleatorio];
                }

                var codigo = new string(arrCodigo);

                if (!await _udt.UrlSqlRepositorio.AnyAsync(x => x.Codigo == codigo))
                    return codigo;
            }
        }

        #endregion ..:: Fim - Privados ::..
    }
}
