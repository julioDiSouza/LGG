using MediatR;

namespace LGG.Aplicacao.Library.Mediator.CampeonaMensal.Commands
{
    public class ApagarCampeonatoMensalCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
