using FluentValidation;
using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.Validators
{
    public class RequisicaoUrlModelVld : AbstractValidator<UrlModelRequisicao>
    {
        public RequisicaoUrlModelVld()
        {
            RuleFor(x => x.UrlLonga)
            .NotEmpty().WithMessage("UrlLonga é obrigatório")
            .Must(x => x?.Length <= 1000).WithMessage("UrlLonga deve conter menos que 1000 caracteres");
        }
    }
}