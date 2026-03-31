using FluentValidation.Results;
using LGG.Aplicacao.Library.Dtos;
using LGG.Aplicacao.Library.Validadores;
using LGG.Aplicacao.Library.Mediator.Videos.Commands;

namespace LGG.Aplicacao.Library.Helpers
{
    public static class VideoHelper
    {
        public static ValidationResult Validacao(this VideoPublicacaoDto video)
        {
            return new VideoPublicacaoDtoValidador().Validate(video);
        }

        public static PublicarVideoCommand ComandoPublicar(this VideoPublicacaoDto video)
        {
            return new PublicarVideoCommand(video.UsuarioId, video.Link);
        }
    }
}
