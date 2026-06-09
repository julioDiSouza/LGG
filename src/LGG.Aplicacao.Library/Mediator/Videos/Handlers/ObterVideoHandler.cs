using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Aplicacao.Library.Mediator.Videos.Queries;

namespace LGG.Aplicacao.Library.Mediator.Videos.Handlers
{
    public class ObterVideoHandler : IRequestHandler<ObterVideoQuery, VideoConsultaDto>
    {
        //private readonly IVideoRepositorio _videoRepositorio;
        //
        //public ObterVideoHandler(IVideoRepositorio videoRepositorio)
        //{
        //    _videoRepositorio = videoRepositorio;
        //}

        public async Task<VideoConsultaDto> Handle(ObterVideoQuery request, CancellationToken cancellationToken)
        {
            //var video = await _videoRepositorio.ObterPorIdAsync(request.Id);
            //
            //if (video == null)
            //    return null;
            //
            //return new VideoConsultaDto
            //{
            //    Id = video.Id,
            //    Titulo = video.Titulo,
            //    Descricao = video.Descricao,
            //    Url = video.Url,
            //    CanalId = video.CanalId,
            //    QtdView = video.QtdView,
            //    QtdLike = video.QtdLike,
            //    UrlImagem = video.UrlImagem,
            //    DataPublicacao = video.DataPublicacao
            //};

            return null;
        }
    }
}
