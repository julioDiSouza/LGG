using EncurtadorLink.Model;
using EncurtadorLink.DataBase;
using Microsoft.EntityFrameworkCore;

namespace EncurtadorLink.Services
{
    public class UrlEncurtadorService
    {
        private readonly int _tamanho;
        private readonly string _caracteres;
        private readonly Random _random = new();
        private readonly ContextoDb _dbContexto;

        public UrlEncurtadorService(ContextoDb dbContexto, ConfiguracaoCodigoUnico configuracaoCodigoUnico)
        {
            _dbContexto= dbContexto;
            _tamanho= configuracaoCodigoUnico.Tamanho;
            _caracteres = configuracaoCodigoUnico.Caracteres;
        }

        public async Task<string> GerarCodigoUnico()
        {
            var arrCodigo = new char[_tamanho];

            while (true)
            {
                for (int i = 0; i < _tamanho; i++)
                {
                    var indexAleatorio = _random.Next(_caracteres.Length);

                    arrCodigo[i] = _caracteres[indexAleatorio];
                }

                var codigo = new string(arrCodigo);

                if (!await _dbContexto.UrlEncurtadas.AnyAsync(u => u.Codigo == codigo))
                    return codigo;
            }
        }
    }
}
