using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.CampeonaMensal.Queries;

namespace LGG.Aplicacao.Library.Mediator.CampeonaMensal.Handlers
{
    public class ObterCampeonatoMensalHandler : IRequestHandler<ObterCampeonatoMensalQuery, CampeonatoMensalConsultaDto>
    {
        private readonly IUnidTrabalho _ut;

        public ObterCampeonatoMensalHandler(IUnidTrabalho ut)
        {
            _ut = ut;
        }

        public async Task<CampeonatoMensalConsultaDto> Handle(ObterCampeonatoMensalQuery request, CancellationToken cancellationToken)
        {
            var campeonatoMensal = await _ut.CampeonatoMensalRepositorio.ObterPorIdAsync(request.Id);

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
