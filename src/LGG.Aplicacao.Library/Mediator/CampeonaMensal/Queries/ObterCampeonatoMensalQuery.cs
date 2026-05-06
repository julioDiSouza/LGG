using MediatR;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Mediator.CampeonaMensal.Queries
{
    public class ObterCampeonatoMensalQuery : IRequest<CampeonatoMensalConsultaDto>
    {
        public int Id { get; set; }
    }
}
