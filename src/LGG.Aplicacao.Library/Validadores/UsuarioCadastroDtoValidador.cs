using FluentValidation;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Validadores
{
    public class UsuarioCadastroDtoValidador : AbstractValidator<UsuarioCadastroDto>
    {
        public UsuarioCadastroDtoValidador()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório.");

            //RuleFor(x => x.CPF)
            //    .NotEmpty()
            //    .WithMessage("O CPF é obrigatório.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O e-mail é obrigatório.")
                .EmailAddress()
                .WithMessage("O e-mail informado não é válido.");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("Senha é obrigatória.");

            RuleFor(x => x.Resenha)
                .NotEmpty().WithMessage("A confirmação de senha é obrigatória.")
                .Equal(x => x.Senha).WithMessage("As senhas não coincidem.");

            RuleFor(x => x.DataNascimento)
                .GreaterThanOrEqualTo(new DateTime(1753, 1, 1))
                .WithMessage("A data de nascimento é obrigatória.");
        }
    }
}
