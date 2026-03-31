using FluentValidation;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Validadores
{
    public class VideoPublicacaoDtoValidador : AbstractValidator<VideoPublicacaoDto>
    {
        public VideoPublicacaoDtoValidador()
        {
            RuleFor(x => x.UsuarioId)
                .NotEmpty()
                .WithMessage("UsuarioId é obrigatório.")
                .MaximumLength(36);

            RuleFor(x => x.Link)
                .NotEmpty()
                .WithMessage("O link do vídeo é obrigatório.");
        }
    }
}
