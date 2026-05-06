using FluentValidation;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Validadores
{
    public class CampeonatoMensalAtualizarDtoValidador : AbstractValidator<CampeonatoMensalAtualizarDto>
    {
        public CampeonatoMensalAtualizarDtoValidador()
        {
            RuleFor(x => x.Mes)
                .NotEmpty()
                .WithMessage("O mês é obrigatório.");

            RuleFor(x => x.Ano)
                .GreaterThan(0)
                .WithMessage("O ano é obrigatório e deve ser maior que zero.")
                .LessThanOrEqualTo(DateTime.Now.Year + 1)
                .WithMessage("O ano não pode ser superior ao próximo ano.");

            RuleFor(x => x.DataInicio)
                .NotEmpty()
                .WithMessage("A data de início é obrigatória.")
                .GreaterThanOrEqualTo(new DateTime(1753, 1, 1))
                .WithMessage("A data de início não é válida.");

            RuleFor(x => x.DataFim)
                .NotEmpty()
                .WithMessage("A data de término é obrigatória.")
                .GreaterThanOrEqualTo(new DateTime(1753, 1, 1))
                .WithMessage("A data de término não é válida.")
                .GreaterThan(x => x.DataInicio)
                .WithMessage("A data de término deve ser após a data de início.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("O status é obrigatório.");
        }
    }
}
