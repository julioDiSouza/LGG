using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Mensagem;
using LGG.Global.Library.Enumeradores;
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

            var videosTopDez = await _ut.VideoTopDezRepositorio.ObterAsync();

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

            topDezDto.Mensagem = new Mensagem(SistemaEnum.ProcessadoSucesso);

            return topDezDto;
        }
    }
}
