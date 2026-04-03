using LGG.Aplicacao.Library.Dtos;
using LGG.Aplicacao.Library.Mediator.Videos.Queries;
using LGG.Dominio.Library.Interfaces.IRepositorios.IVideo;
using MediatR;

namespace LGG.Aplicacao.Library.Mediator.Videos.Handlers
{
    public class ObterTodosVideosHandler : IRequestHandler<ObterTodosVideosQuery, List<VideoConsultaDto>>
    {
        //private readonly IVideoRepositorio _videoRepositorio;
        //
        //public ObterTodosVideosHandler(IVideoRepositorio videoRepositorio)
        //{
        //    _videoRepositorio = videoRepositorio;
        //}

        public async Task<List<VideoConsultaDto>> Handle(ObterTodosVideosQuery request, CancellationToken cancellationToken)
        {
            //var videos = await _videoRepositorio.ObterTodosAsync();
            //
            //return videos.Select(v => new VideoConsultaDto
            //{
            //    Id = v.Id,
            //    Titulo = v.Titulo,
            //    Descricao = v.Descricao,
            //    Url = v.Url,
            //    CanalId = v.CanalId,
            //    QtdView = v.QtdView,
            //    QtdLike = v.QtdLike,
            //    UrlImagem = v.UrlImagem,
            //    DataPublicacao = v.DataPublicacao
            //}).ToList();

            return null;
        }
    }
}
