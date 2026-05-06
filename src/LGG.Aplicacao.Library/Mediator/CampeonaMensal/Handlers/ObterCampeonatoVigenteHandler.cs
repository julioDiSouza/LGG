using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.CampeonaMensal.Queries;

namespace LGG.Aplicacao.Library.Mediator.CampeonaMensal.Handlers
{
    public class ObterCampeonatoVigenteHandler : IRequestHandler<ObterCampeonatoVigenteQuery, CampeonatoMensalConsultaDto>
    {
        private readonly IUnidTrabalho _ut;

        public ObterCampeonatoVigenteHandler(IUnidTrabalho ut)
        {
            _ut = ut;
        }

        public async Task<CampeonatoMensalConsultaDto> Handle(ObterCampeonatoVigenteQuery request, CancellationToken cancellationToken)
        {
            var campeonatoMensal = await _ut.CampeonatoMensalRepositorio.ObterVigenteAsync();

            if (campeonatoMensal == null)
                return null;

            return new CampeonatoMensalConsultaDto
            {
                Id = campeonatoMensal.Id,
                Mes = campeonatoMensal.Mes,
                Ano = campeonatoMensal.Ano,
                DataInicio = campeonatoMensal.DataInicio,
                DataFim = campeonatoMensal.DataFim,
                Status = campeonatoMensal.Status,
                DataCadastro = campeonatoMensal.DataCadastro
            };
        }
    }
}
