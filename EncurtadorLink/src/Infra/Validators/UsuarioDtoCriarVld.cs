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
            .WithMessage("NomeUsuario é obrigatório")
            .Must(x => x?.Length <= 256)
            .WithMessage("NomeUsuario deve conter menos que 256 caracteres");

            RuleFor(x => x.NomeCompleto)
            .NotEmpty()
            .WithMessage("NomeCompleto é obrigatório")
            .Must(x => x?.Length <= 256)
            .WithMessage("NomeCompleto deve conter menos que 256 caracteres");

            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email é obrigatório")
            .Must(x => x?.Length <= 256)
            .WithMessage("Email deve conter menos que 256 caracteres");

            RuleFor(x => x.ClienteId)
            .NotEmpty()
            .WithMessage("ClienteId é obrigatório");

            RuleFor(x => x.Senha)
            .NotEmpty()
            .WithMessage("Senha é obrigatório");
        }
    }
}