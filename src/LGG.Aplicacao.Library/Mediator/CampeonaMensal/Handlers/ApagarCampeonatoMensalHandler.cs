using MediatR;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.CampeonaMensal.Commands;

namespace LGG.Aplicacao.Library.Mediator.CampeonaMensal.Handlers
{
    public class ApagarCampeonatoMensalHandler : IRequestHandler<ApagarCampeonatoMensalCommand, Unit>
    {
        private readonly IUnidTrabalho _ut;

        public ApagarCampeonatoMensalHandler(IUnidTrabalho ut)
        {
            _ut = ut;
        }

        public async Task<Unit> Handle(ApagarCampeonatoMensalCommand request, CancellationToken cancellationToken)
        {
            await _ut.CampeonatoMensalRepositorio.RemoverAsync(request.Id);

            return Unit.Value;
        }
    }
}
