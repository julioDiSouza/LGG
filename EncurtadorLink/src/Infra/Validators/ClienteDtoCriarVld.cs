using EncurtadorLink.Model.Usuario;
using FluentValidation;

namespace EncurtadorLink.Infra.Validators
{
    public class ClienteDtoCriarVld : AbstractValidator<ClienteDtoCriar>
    {
        public ClienteDtoCriarVld()
        {
            RuleFor(x => x.RazaoSocial)
            .NotEmpty()
            .WithMessage("Campo obrigatório: RazaoSocial");

            RuleFor(x => x.NomeCompleto)
            .NotEmpty()
            .WithMessage("Campo obrigatório: NomeCompleto")
            .Must(x => x.Length <= 50)
            .WithMessage("NomeCompleto deve conter menos que 50 caracteres");

            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Campo obrigatório: Email")
            .Must(x => x.Length <= 50)
            .WithMessage("Email deve conter menos que 50 caracteres");

            RuleFor(x => x.Documento)
            .NotEmpty()
            .WithMessage("Campo obrigatório: Documento")
            .Must(x => x.Length >= 11 || x.Length <= 14)
            .WithMessage("Documento deve conter entre 11 e 14 caracteres");
        }
    }
}
