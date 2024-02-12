using EncurtadorLink.Model.Requisicao;
using FluentValidation;

namespace EncurtadorLink.Infra.Validators
{
    public class LoginRequisicaoModelVld : AbstractValidator<LoginRequisicaoModel>
    {
        public LoginRequisicaoModelVld()
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
