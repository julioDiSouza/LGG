using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Extensoes;
using LGG.Aplicacao.Library.Helpers;
using LGG.Global.Library.Enumeradores;
using LGG.Aplicacao.Library.Dtos.Youtube;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.Videos.Commands;
using LGG.Aplicacao.Library.Mediator.Usuarios.Queries;
using LGG.Aplicacao.Library.Mediator.CampeonaMensal.Queries;
using LGG.Aplicacao.Library.Interfaces.IServices;

namespace LGG.Aplicacao.Library.Mediator.Videos.Handlers
{
    public class PublicarVideoHandler : IRequestHandler<PublicarVideoCommand, SistemaEnum>
    {
        private readonly IMediator _mediator;
        private readonly IYoutubeService _ytService;
        private readonly IUnidTrabalho _ut;

        public PublicarVideoHandler(IMediator mediator, IYoutubeService ytService, IUnidTrabalho ut)
        {
            _mediator = mediator;
            _ytService = ytService;
            _ut = ut;
        }

        public async Task<SistemaEnum> Handle(PublicarVideoCommand request, CancellationToken cancellationToken)
        {
            // Verificar se o usuário existe e está ativo
            var usuario = await _mediator.Send(new ObterUsuarioQuery { Id = request.UsuarioId });
            if (usuario == null || usuario.Status != UsuarioStatusEnum.Ativo)
                return SistemaEnum.UsuarioInvalido;

            // Verificar se há um campeonato mensal vigente e aberto
            CampeonatoMensalConsultaDto campeonatoVigente = await _mediator.Send(new ObterCampeonatoVigenteQuery());
            if (campeonatoVigente == null ||
                campeonatoVigente.Status != CampeonatoMensalStatusEnum.Aberto)
                return SistemaEnum.CampeonatoIndisponivel;

            var videoId = request.Link.ObterVideoId();
            var videoResponse =  await _ytService.ObterVideosPorIdAsync(videoId);
            var videoValido = VideoValido(videoResponse, usuario.CodigoVideo);
            if (!videoValido)
                return SistemaEnum.NaoEncontrado;

            // Verificar se o usuário já publicou um vídeo para o campeonato vigente
            var videoExistente = await _ut.VideoRepositorio.ObterPorUsuarioIdCampIdAsync(usuario.Id.ToString(), campeonatoVigente.Id);
            if (videoExistente != null)
                return SistemaEnum.VideoJaPublicado;

            // Gravar o vídeo no banco de dados
            var video = videoResponse!.ConverterParaVideo();
            video.UsuarioId = new Guid(request.UsuarioId);
            video.CampeonatoMensalId = campeonatoVigente.Id;
            video.VideoCicloId = 0;
            video.Url = request.Link;
            video = await _ut.VideoRepositorio.AdicionarAsync(video);

            return SistemaEnum.Sucesso;
        }

        private bool VideoValido(VideoListResponseDto? video, string codigoVideo)
        {
            var valido = false;

            if (video == null || video.Items == null || video.Items.Count == 0)
                return valido;

            valido = video.Items.Any(item => 
                item.Snippet != null && 
                !string.IsNullOrEmpty(item.Snippet.Description) &&
                item.Snippet.Description.Contains(codigoVideo));

            return valido;
        }
    }
}
