using EncurtadorLink.Model.Requisicao;
using FluentValidation;

namespace EncurtadorLink.Infra.Validators
{
    public class UrlRequisicaoModelVld : AbstractValidator<UrlRequisicaoModel>
    {
        public UrlRequisicaoModelVld()
        {
            RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Url é obrigatório")
            .Must(x => x?.Length <= 1000).WithMessage("Url deve conter menos que 1000 caracteres");
        }
    }
}