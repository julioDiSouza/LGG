using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.CampeonaMensal.Queries;

namespace LGG.Aplicacao.Library.Mediator.CampeonaMensal.Handlers
{
    public class ObterTodosCampeonatosHandler : IRequestHandler<ObterTodosCampeonatosQuery, List<CampeonatoMensalConsultaDto>>
    {
        private readonly IUnidTrabalho _ut;

        public ObterTodosCampeonatosHandler(IUnidTrabalho ut)
        {
            _ut = ut;
        }

        public async Task<List<CampeonatoMensalConsultaDto>> Handle(ObterTodosCampeonatosQuery request, CancellationToken cancellationToken)
        {
            var campeonatos = await _ut.CampeonatoMensalRepositorio.ObterTodosAsync();

            if (campeonatos == null || !campeonatos.Any())
                return null;

            return campeonatos
                .Select(c => new CampeonatoMensalConsultaDto
                {
                    Id = c.Id,
                    Mes = c.Mes,
                    Ano = c.Ano,
                    DataInicio = c.DataInicio,
                    DataFim = c.DataFim,
                    Status = c.Status,
                    DataCadastro = c.DataCadastro
                })
                .ToList();
        }
    }
}
