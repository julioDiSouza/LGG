using FluentValidation;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Validadores
{
    public class CanalValidacaoDtoValidador : AbstractValidator<CanalValidacaoDto>
    {
        public CanalValidacaoDtoValidador()
        {
            RuleFor(x => x.UsuarioId)
                .NotEmpty()
                .WithMessage("UsuarioId é obrigatório.")
                .MaximumLength(36);

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome do canal é obrigatório.");
        }
    }
}