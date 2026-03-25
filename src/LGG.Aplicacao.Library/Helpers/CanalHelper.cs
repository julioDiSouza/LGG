using FluentValidation.Results;
using LGG.Aplicacao.Library.Dtos;
using LGG.Aplicacao.Library.Validadores;
using LGG.Aplicacao.Library.Mediator.Canais.Commands;

namespace LGG.Aplicacao.Library.Helpers
{
    public static class CanalHelper
    {
        public static ValidationResult Validacao(this CanalValidacaoDto canal)
        {
            return new CanalValidacaoDtoValidador().Validate(canal);
        }

        public static ValidarCanalCommand ComandoValidar(this CanalValidacaoDto canal)
        {
            return new ValidarCanalCommand(canal.UsuarioId, canal.Nome);
        }
    }
}
