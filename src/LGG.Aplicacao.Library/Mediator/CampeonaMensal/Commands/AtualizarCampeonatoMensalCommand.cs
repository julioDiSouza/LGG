using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Enumeradores;

namespace LGG.Aplicacao.Library.Mediator.CampeonaMensal.Commands
{
    public class AtualizarCampeonatoMensalCommand : IRequest<CampeonatoMensalConsultaDto>
    {
        public int Id { get; set; }
        public MesEnum Mes { get; set; }
        public int Ano { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public CampeonatoMensalStatusEnum Status { get; set; }
    }
}
