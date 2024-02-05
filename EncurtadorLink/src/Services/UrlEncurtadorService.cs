using EncurtadorLink.Entidades;
using Microsoft.EntityFrameworkCore;
using EncurtadorLink.Infra.BancoDados;
using EncurtadorLink.Services.Interface;

namespace EncurtadorLink.Services
{
    public class UrlEncurtadorService : IUrlEncurtadorService
    {
        private readonly int _tamanho;
        private readonly string _caracteres;
        private readonly ContextoDb _dbContexto;

        public UrlEncurtadorService(IConfiguration configuration, ContextoDb dbContexto)
        {
            _dbContexto= dbContexto;
            _tamanho= int.Parse(configuration["ConfiguracaoCodigoUnico:Tamanho"]!);
            _caracteres= configuration["ConfiguracaoCodigoUnico:Caracteres"]!;
        }

        public async Task<UrlEncurtada> GerarCodigoUnicoAsync(UrlEncurtada urlEncurtada)
        {
            urlEncurtada.Codigo = await ObterCodigoUnicoAsync();
            urlEncurtada.UrlCurta += urlEncurtada.Codigo;

            _dbContexto.UrlEncurtadas.Add(urlEncurtada);

            await _dbContexto.SaveChangesAsync();

            return urlEncurtada;
        }

        public async Task<UrlEncurtada> RecuperarLinkAsync(string codigo) => await _dbContexto.UrlEncurtadas
                .FirstOrDefaultAsync(u => u.Codigo == codigo);

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

                if (!await _dbContexto.UrlEncurtadas.AnyAsync(u => u.Codigo == codigo))
                    return codigo;
            }
        }

        #endregion ..:: Fim - Privados ::..

    }
}
