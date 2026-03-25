using FluentValidation;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Validadores
{
    public class UsuarioLoginRequisicaoDtoValidador : AbstractValidator<UsuarioLoginRequisicaoDto>
    {
        public UsuarioLoginRequisicaoDtoValidador()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O e-mail é obrigatório.")
                .EmailAddress()
                .WithMessage("O e-mail informado não é válido.");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("Senha é obrigatória.");
        }
    }
}
