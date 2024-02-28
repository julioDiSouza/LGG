using FluentValidation;
using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.CriaLink.Function.Validators
{
    public class RequisicaoUrlModelVld : AbstractValidator<RequisicaoUrlModel>
    {
        public RequisicaoUrlModelVld()
        {
            RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Url é obrigatório")
            .Must(x => x?.Length <= 1000).WithMessage("Url deve conter menos que 1000 caracteres");
        }
    }
}
