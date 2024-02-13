using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncurtadorLink.Services.Library.Classes
{
    public class CodigoUnico
    {
        private readonly int _tamanho;
        private readonly string _caracteres;

        public CodigoUnico(IConfiguration configuration)
        {
            _tamanho = int.Parse(configuration["ConfiguracaoCodigoUnico:Tamanho"]!);
            _caracteres = configuration["ConfiguracaoCodigoUnico:Caracteres"]!;
        }

        public async Task<string> ObterCodigoUnicoAsync()
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
    }
}
