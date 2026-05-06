using MediatR;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.CampeonaMensal.Commands;

namespace LGG.Aplicacao.Library.Mediator.CampeonaMensal.Handlers
{
    public class AtualizarCampeonatoMensalHandler : IRequestHandler<AtualizarCampeonatoMensalCommand, LGG.Aplicacao.Library.Dtos.CampeonatoMensalConsultaDto>
    {
        private readonly IUnidTrabalho _ut;

        public AtualizarCampeonatoMensalHandler(IUnidTrabalho ut)
        {
            _ut = ut;
        }

        public async Task<LGG.Aplicacao.Library.Dtos.CampeonatoMensalConsultaDto> Handle(AtualizarCampeonatoMensalCommand request, CancellationToken cancellationToken)
        {
            var campeonatoMensal = await _ut.CampeonatoMensalRepositorio.ObterPorIdAsync(request.Id);

            if (campeonatoMensal == null)
                return null;

            campeonatoMensal.Mes = request.Mes;
            campeonatoMensal.Ano = request.Ano;
            campeonatoMensal.DataInicio = request.DataInicio;
            campeonatoMensal.DataFim = request.DataFim;
            campeonatoMensal.Status = request.Status;

            await _ut.CampeonatoMensalRepositorio.AtualizarAsync(campeonatoMensal);

            return new LGG.Aplicacao.Library.Dtos.CampeonatoMensalConsultaDto
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
