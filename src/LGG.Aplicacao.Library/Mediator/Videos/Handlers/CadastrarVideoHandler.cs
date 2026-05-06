using LGG.Aplicacao.Library.Dtos;
using LGG.Aplicacao.Library.Mediator.Videos.Commands;
using LGG.Dominio.Library.Entidades;
using LGG.Dominio.Library.Interfaces.IRepositorios.IVideo;
using LGG.Global.Library.Erros;
using LGG.Global.Library.Enumeradores;
using MediatR;

namespace LGG.Aplicacao.Library.Mediator.Videos.Handlers
{
    public class CadastrarVideoHandler : IRequestHandler<CadastrarVideoCommand, VideoCadastroRetornoDto>
    {
        //private readonly IVideoRepositorio _videoRepositorio;
        //
        //public CadastrarVideoHandler(IVideoRepositorio videoRepositorio)
        //{
        //    _videoRepositorio = videoRepositorio;
        //}

        public async Task<VideoCadastroRetornoDto> Handle(CadastrarVideoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var video = new Video();

                //await _videoRepositorio.AdicionarAsync(video);

                return new VideoCadastroRetornoDto
                {
                    Id = video.Id,
                    Titulo = video.Titulo,
                    Url = video.Url,
                };
            }
            catch (Exception ex)
            {
                return new VideoCadastroRetornoDto
                {
                    Erro = new Erro(SistemaEnum.ErroInterno)
                };
            }
        }
    }
}
