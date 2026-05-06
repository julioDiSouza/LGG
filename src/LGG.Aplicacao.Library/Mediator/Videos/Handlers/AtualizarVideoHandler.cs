using LGG.Aplicacao.Library.Dtos;
using LGG.Aplicacao.Library.Mediator.Videos.Commands;
using LGG.Dominio.Library.Interfaces.IRepositorios.IVideo;
using MediatR;

namespace LGG.Aplicacao.Library.Mediator.Videos.Handlers
{
    public class AtualizarVideoHandler : IRequestHandler<AtualizarVideoCommand, VideoAtualizarDto>
    {
        //private readonly IVideoRepositorio _videoRepositorio;

        public AtualizarVideoHandler(/*IVideoRepositorio videoRepositorio*/)
        {
            //_videoRepositorio = videoRepositorio;
        }

        public async Task<VideoAtualizarDto> Handle(AtualizarVideoCommand request, CancellationToken cancellationToken)
        {
            /*
            var video = await _videoRepositorio.AtualizarAsync(request.Id, request.Titulo, request.Descricao, request.Url);

            if (video == null)
                return null;

            return new VideoAtualizarDto
            {
                Id = video.Id,
                Titulo = video.Titulo,
                Descricao = video.Descricao,
                Url = video.Url
            };
            */

            return null;
        }
    }
}
