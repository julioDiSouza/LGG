using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Aplicacao.Library.Helpers;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.Canais.Queries;

namespace LGG.Aplicacao.Library.Mediator.Canais.Handlers
{
    public class ObterCanalHandler : IRequestHandler<ObterCanalQuery, CanalDto?>
    {
        private readonly IUnidTrabalho _ut;

        public ObterCanalHandler(IUnidTrabalho ut)
        {
            _ut = ut;
        }

        public async Task<CanalDto?> Handle(ObterCanalQuery request, CancellationToken cancellationToken)
        {
            var canal = await _ut.CanalRepositorio.ObterPorCanalIdAsync(request.CanalId);

            return canal?.ConverterParaCanalDto();
        }
    }
}
