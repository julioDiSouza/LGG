using EncurtadorLink.Entidades;
using FluentValidation;
using System.Text.RegularExpressions;

namespace EncurtadorLink.Infra.Validators
{
    public class ApplicationUserVld : AbstractValidator<ApplicationUser>
    {
        public ApplicationUserVld()
        {
            RuleFor(x => x.ClienteId)
            .NotEmpty()
            .WithMessage("ClienteId é obrigatório");

            RuleFor(x => x.NomeCompleto)
            .NotEmpty()
            .WithMessage("NomeCompleto é obrigatório")
            .Must(x => x?.Length <= 256)
            .WithMessage("NomeCompleto deve conter menos que 256 caracteres");
        }

        public bool MaximoParaTodos(List<string> valores, int tamanho)
        {
            var matches = valores.Select(p => p.Length == tamanho);

            return matches.Count(p => !p) == 0;
        }

        public bool ApenasNumerosParaTodos(List<string> valores)
        {
            var matches = valores.Select(p => Regex.IsMatch(p, @"^(\d{0,})$"));

            return matches.Count(p => !p) == 0;
        }
    }
}