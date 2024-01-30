using EncurtadorLink.DataBase;
using Microsoft.EntityFrameworkCore;

namespace EncurtadorLink.Services
{
    public class UrlEncurtadorService
    {
        public const int Tamanho = 5;
        public const string Caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-";

        private readonly Random _random = new();
        private readonly ContextoDb _dbContexto;

        public UrlEncurtadorService(ContextoDb dbContexto)
        {
            _dbContexto = dbContexto;
        }

        public async Task<string> GerarCodigoUnico()
        {
            var arrCodigo = new char[Tamanho];

            while (true)
            {
                for (int i = 0; i < Tamanho; i++)
                {
                    var indexAleatorio = _random.Next(Caracteres.Length);

                    arrCodigo[i] = Caracteres[indexAleatorio];
                }

                var codigo = new string(arrCodigo);

                if (!await _dbContexto.UrlEncurtadas.AnyAsync(u => u.Codigo == codigo))
                    return codigo;
            }
        }
    }
}
