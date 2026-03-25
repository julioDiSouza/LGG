using FluentValidation;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Validadores
{
    public class UsuarioAtualizarDtoValidador : AbstractValidator<UsuarioAtualizarDto>
    {
        public UsuarioAtualizarDtoValidador()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório.");

            RuleFor(x => x.DataNascimento)
                .GreaterThanOrEqualTo(new DateTime(1753, 1, 1))
                .WithMessage("A data de nascimento é obrigatória.");
        }
    }
}
