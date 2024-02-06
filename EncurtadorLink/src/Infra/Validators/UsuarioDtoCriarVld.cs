using EncurtadorLink.Model.Usuario;
using FluentValidation;

namespace EncurtadorLink.Infra.Validators
{
    public class UsuarioDtoCriarVld : AbstractValidator<UsuarioDtoCriar>
    {
        public UsuarioDtoCriarVld()
        {
            RuleFor(u => u.NomeUsuario)
            .NotEmpty()
            .WithMessage("Campo obrigatório: NomeUsuario");

            RuleFor(u => u.NomeCompleto)
            .NotEmpty()
            .WithMessage("Campo obrigatório: NomeCompleto");

            RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Campo obrigatório: Email");

            RuleFor(u => u.ClienteId)
            .NotEmpty()
            .WithMessage("Campo obrigatório: ClienteId");

            RuleFor(u => u.Senha)
            .NotEmpty()
            .WithMessage("Campo obrigatório: Senha");
        }
    }
}