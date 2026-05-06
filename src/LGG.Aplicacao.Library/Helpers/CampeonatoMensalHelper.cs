using FluentValidation.Results;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Enumeradores;
using LGG.Aplicacao.Library.Validadores;
using LGG.Aplicacao.Library.Mediator.CampeonaMensal.Commands;

namespace LGG.Aplicacao.Library.Helpers
{
    public static class CampeonatoMensalHelper
    {
        public static ValidationResult Validacao(this CampeonatoMensalCadastroDto campeonatoMensal)
        {
            return new CampeonatoMensalCadastroDtoValidador().Validate(campeonatoMensal);
        }

        public static ValidationResult Validacao(this CampeonatoMensalAtualizarDto campeonatoMensal)
        {
            return new CampeonatoMensalAtualizarDtoValidador().Validate(campeonatoMensal);
        }

        public static CadastrarCampeonatoMensalCommand ConverterComandoCadastrar(this CampeonatoMensalCadastroDto campeonatoMensal)
        {
            return new CadastrarCampeonatoMensalCommand
            {
                Mes = campeonatoMensal.Mes,
                Ano = campeonatoMensal.Ano,
                DataInicio = campeonatoMensal.DataInicio,
                DataFim = campeonatoMensal.DataFim,
                Status = CampeonatoMensalStatusEnum.Aguardando
            };
        }

        public static AtualizarCampeonatoMensalCommand ConverterComandoAtualizar(this CampeonatoMensalAtualizarDto campeonatoMensal, int id)
        {
            return new AtualizarCampeonatoMensalCommand
            {
                Id = id,
                Mes = campeonatoMensal.Mes,
                Ano = campeonatoMensal.Ano,
                DataInicio = campeonatoMensal.DataInicio,
                DataFim = campeonatoMensal.DataFim,
                Status = campeonatoMensal.Status
            };
        }
    }
}
