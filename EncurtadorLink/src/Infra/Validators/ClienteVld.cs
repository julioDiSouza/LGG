using EncurtadorLink.Entidades;
using FluentValidation;

namespace EncurtadorLink.Infra.Validators
{
    public class ClienteVld : AbstractValidator<Cliente>
    {
        public ClienteVld()
        {
            RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Campo obrigatório: Id");

            RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Campo obrigatório: Nome")
            .Must(x => x.Length <= 50)
            .WithMessage("Nome deve conter menos que 50 caracteres"); 

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
