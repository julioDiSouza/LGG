using FluentValidation.Results;
using LGG.Aplicacao.Library.Dtos;
using LGG.Aplicacao.Library.Validadores;
using LGG.Aplicacao.Library.Mediator.Videos.Commands;
using LGG.Aplicacao.Library.Mediator.Videos.Queries;

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

        public static ValidationResult ValidacaoCadastro(this VideoCadastroDto dto)
            => new VideoCadastroDtoValidador().Validate(dto);

        public static ValidationResult ValidacaoAtualizacao(this VideoAtualizarDto dto)
            => new VideoAtualizarDtoValidador().Validate(dto);

        public static CadastrarVideoCommand ComandoCadastrar(this VideoCadastroDto dto)
            => new CadastrarVideoCommand
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Url = dto.Url,
                CanalId = dto.CanalId
            };

        public static AtualizarVideoCommand ComandoAtualizar(this VideoAtualizarDto dto)
            => new AtualizarVideoCommand
            {
                Id = dto.Id,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Url = dto.Url
            };

        public static ApagarVideoCommand ComandoApagar(long id)
            => new ApagarVideoCommand { Id = id };

        public static ObterVideoQuery QueryObter(long id)
            => new ObterVideoQuery { Id = id };
    }
}
