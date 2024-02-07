using EncurtadorLink.Model.Usuario;
using FluentValidation;

namespace EncurtadorLink.Infra.Validators
{
    public class UsuarioDtoCriarVld : AbstractValidator<UsuarioDtoCriar>
    {
        public UsuarioDtoCriarVld()
        {
            RuleFor(x => x.NomeUsuario)
            .NotEmpty()
            .WithMessage("Campo obrigatório: NomeUsuario")
            .Must(x => x.Length <= 256)
            .WithMessage("NomeUsuario deve conter menos que 256 caracteres");

            RuleFor(x => x.NomeCompleto)
            .NotEmpty()
            .WithMessage("Campo obrigatório: NomeCompleto")
            .Must(x => x.Length <= 256)
            .WithMessage("NomeCompleto deve conter menos que 256 caracteres");

            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Campo obrigatório: Email")
            .Must(x => x.Length <= 256)
            .WithMessage("Email deve conter menos que 256 caracteres");

            RuleFor(x => x.ClienteId)
            .NotEmpty()
            .WithMessage("Campo obrigatório: ClienteId");

            RuleFor(x => x.Senha)
            .NotEmpty()
            .WithMessage("Campo obrigatório: Senha");
        }
    }
}