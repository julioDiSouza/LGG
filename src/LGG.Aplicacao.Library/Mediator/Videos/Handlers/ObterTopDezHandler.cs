using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Dominio.Library.Entidades;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.Videos.Queries;

namespace LGG.Aplicacao.Library.Mediator.Videos.Handlers
{
    public class ObterTopDezHandler : IRequestHandler<ObterTopDezQuery, TopDezDto>
    {
        private readonly IUnidTrabalho _ut;

        public ObterTopDezHandler(IUnidTrabalho ut)
        {
            _ut = ut;
        }

        public async Task<TopDezDto> Handle(ObterTopDezQuery request, CancellationToken cancellationToken)
        {
            var campeonatoMensal = await _ut.CampeonatoMensalRepositorio.ObterVigenteAsync();

            if (campeonatoMensal == null)
                return new TopDezDto();

            var ultimoCiclo = await _ut.VideoCicloRepositorio.ObterUltimoVideoCicloAsync();
            var videosTopDez = await ObterTopDezAtualAsync(campeonatoMensal!.Id, ultimoCiclo!.Id);

            var topDezDto = new TopDezDto();
            topDezDto.CampeonatoMensalId = campeonatoMensal.Id;
            topDezDto.VideoCicloId = videosTopDez.FirstOrDefault()?.VideoCicloId ?? 0;
            topDezDto.DataAtualizacao = videosTopDez.FirstOrDefault()?.DataCadastro ?? DateTime.MinValue;

            var posicao = 1;
            foreach (var videoTopDez in videosTopDez)
            {
                topDezDto.Videos.Add(new VideoTopDezDto
                {
                    VideoId = videoTopDez.VideoId,
                    UsuarioId = videoTopDez.UsuarioId,
                    UsuarioNome = videoTopDez.UsuarioNome,
                    Titulo = videoTopDez.Titulo,
                    Url = videoTopDez.Url,
                    QtdView = videoTopDez.QtdView,
                    QtdLike = videoTopDez.QtdLike,
                    UrlImagem = videoTopDez.UrlImagem,
                    Posicao = posicao
                });

                posicao++;
            }

            return topDezDto;
        }

        private async Task<List<VideoTopDez>> ObterTopDezAtualAsync(int campeonatoMensalId, long videoCicloId)
        {
            if (videoCicloId <= 0)
                return new List<VideoTopDez>();

            var videosTopDez = await _ut.VideoTopDezRepositorio.ObterPorCampeonatoMensalIdAsync(campeonatoMensalId, videoCicloId);

            return videosTopDez.Any() ? videosTopDez : await ObterTopDezAtualAsync(campeonatoMensalId, videoCicloId - 1);
        }
    }
}
