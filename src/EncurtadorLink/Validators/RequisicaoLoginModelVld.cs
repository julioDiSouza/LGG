using FluentValidation;
using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.Validators
{
    public class RequisicaoLoginModelVld : AbstractValidator<LoginModelRequisicao>
    {
        public RequisicaoLoginModelVld()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email com formato inválido")
            .Must(x => x?.Length <= 256).WithMessage("Email deve conter menos que 256 caracteres");

            RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("Senha é obrigatório");
        }
    }
}
