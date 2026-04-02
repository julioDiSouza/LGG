using MediatR;
using LGG.Global.Library.Erros;
using LGG.Aplicacao.Library.Dtos;
using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.CampeonaMensal.Commands;

namespace LGG.Aplicacao.Library.Mediator.CampeonaMensal.Handlers
{
    public class CadastrarCampeonatoMensalHandler : IRequestHandler<CadastrarCampeonatoMensalCommand, CampeonatoMensalCadastroRetornoDto>
    {
        private readonly IUnidTrabalho _ut;

        public CadastrarCampeonatoMensalHandler(IUnidTrabalho ut)
        {
            _ut = ut;
        }

        public async Task<CampeonatoMensalCadastroRetornoDto> Handle(CadastrarCampeonatoMensalCommand request, CancellationToken cancellationToken)
        {
            var campeonatoExistente = await _ut.CampeonatoMensalRepositorio.ObterPorMesEAnoAsync(request.Mes, request.Ano);

            if (campeonatoExistente != null)
            {
                var retornoErro = new CampeonatoMensalCadastroRetornoDto();
                retornoErro.Erro = new Erro(MsgSistemaEnum.CampeonatoMensalJaCadastrado);

                return retornoErro;
            }

            try
            {
                var campeonatoMensal = new CampeonatoMensal
                {
                    Mes = request.Mes,
                    Ano = request.Ano,
                    DataInicio = request.DataInicio,
                    DataFim = request.DataFim,
                    Status = request.Status,
                    DataCadastro = DateTime.Now
                };

                await _ut.CampeonatoMensalRepositorio.AdicionarAsync(campeonatoMensal);

                var retorno = new CampeonatoMensalCadastroRetornoDto
                {
                    Id = campeonatoMensal.Id,
                    Mes = campeonatoMensal.Mes,
                    Ano = campeonatoMensal.Ano,
                    DataInicio = campeonatoMensal.DataInicio,
                    DataFim = campeonatoMensal.DataFim,
                    Status = campeonatoMensal.Status,
                    DataCadastro = campeonatoMensal.DataCadastro
                };

                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
