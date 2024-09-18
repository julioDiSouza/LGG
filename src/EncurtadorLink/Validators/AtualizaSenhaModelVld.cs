using FluentValidation;
using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.Validators
{
    public class AtualizaSenhaModelVld : AbstractValidator<AtualizaSenhaModel>
    {
        public AtualizaSenhaModelVld()
        {
            RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("Email é obrigatório");

            RuleFor(x => x.NovaSenha)
            .NotEmpty().WithMessage("NovaSenha é obrigatória");

            RuleFor(x => x.ReNovaSenha)
            .NotEmpty().WithMessage("ReNovaSenha é obrigatória");
        }
    }
}