using Microsoft.Extensions.Configuration;
using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.UseCases.Library.Services.Interfaces;
using EncurtadorLink.Database.Library.UnidadeTrabalho.Interface;

namespace EncurtadorLink.UseCases.Library.Services
{
    public class UrlService : IUrlService
    {
        private readonly int _tamanho;
        private readonly string _caracteres;
        private readonly IUnidadeTrabalho _udt;

        public UrlService(IConfiguration configuration, IUnidadeTrabalho udt)
        {
            _udt = udt;
            _tamanho = int.Parse(configuration["ConfiguracaoCodigoUnico:Tamanho"]!);
            _caracteres = configuration["ConfiguracaoCodigoUnico:Caracteres"]!;
        }

        public async Task<UrlSql> GerarUrlCodigoUnicoAsync(UrlSql urlSql)
        {
            urlSql.Codigo = await ObterCodigoUnicoAsync();
            urlSql.UrlCurta += urlSql.Codigo;

            await _udt.UrlSqlRepositorio.AdicionarAsync(urlSql);

            return urlSql;
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
