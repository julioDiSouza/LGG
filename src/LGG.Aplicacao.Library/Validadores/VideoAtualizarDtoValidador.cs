using FluentValidation;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Validadores
{
    public class VideoAtualizarDtoValidador : AbstractValidator<VideoAtualizarDto>
    {
        public VideoAtualizarDtoValidador()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ID do vídeo é obrigatório");

            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Título é obrigatório")
                .MaximumLength(255).WithMessage("Título não pode ultrapassar 255 caracteres");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição é obrigatória");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("URL é obrigatória")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("URL inválida");
        }
    }
}
