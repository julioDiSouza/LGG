using MediatR;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Mediator.CampeonaMensal.Queries
{
    public class ObterTodosCampeonatosQuery : IRequest<List<CampeonatoMensalConsultaDto>>
    {
    }
}
