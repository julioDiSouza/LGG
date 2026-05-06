using MediatR;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Mediator.CampeonaMensal.Queries
{
    public class ObterCampeonatosPorAnoQuery : IRequest<List<CampeonatoMensalConsultaDto>>
    {
        public int Ano { get; set; }
    }
}
