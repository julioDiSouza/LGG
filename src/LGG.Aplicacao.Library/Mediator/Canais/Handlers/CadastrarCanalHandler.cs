using MediatR;
using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;
using Microsoft.Extensions.Configuration;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.Canais.Commands;

namespace LGG.Aplicacao.Library.Mediator.Canais.Handlers
{
    public class CadastrarCanalHandler : IRequestHandler<CadastrarCanalCommand, bool>
    {
        private readonly IUnidTrabalho _ut;
        private readonly int _tamanho;
        private readonly string _caracteres;

        public CadastrarCanalHandler(IConfiguration configuration, IUnidTrabalho ut)
        {
            _ut = ut;
            _tamanho = int.Parse(configuration["ConfiguracaoCodigoUnico:Tamanho"]!);
            _caracteres = configuration["ConfiguracaoCodigoUnico:Caracteres"]!;
        }

        public async Task<bool> Handle(CadastrarCanalCommand request, CancellationToken cancellationToken)
        {
            var canal = new Canal
            {
                Id = Guid.NewGuid(),
                UsuarioId = request.UsuarioId,
                Status = CanalStatusEnum.Pendente,
                Codigo = await ObterCodigoUnicoAsync(),
                DataCadastro = DateTime.UtcNow
            };

            await _ut.CanalRepositorio.AdicionarAsync(canal);

            return true;
        }

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

                if (await _ut.CanalRepositorio.CodigoLiberadoAsync(codigo))
                    return codigo;
            }
        }
    }
}

