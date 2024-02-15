using FluentValidation;
using EncurtadorLink.Shared.Library.Models;

namespace EncurtadorLink.Validators
{
    public class ClienteModelVld : AbstractValidator<ClienteModel>
    {
        public ClienteModelVld()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id é obrigatório");

            RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .Must(x => x?.Length <= 50).WithMessage("Nome deve conter menos que 50 caracteres");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email com formato inválido")
            .Must(x => x?.Length <= 50).WithMessage("Email deve conter menos que 50 caracteres");

            RuleFor(x => x.Documento)
            .NotEmpty().WithMessage("Documento é obrigatório")
            .Must(x => x?.Length >= 11 || x?.Length <= 14).WithMessage("Documento deve conter entre 11 e 14 caracteres");
        }
    }
}
