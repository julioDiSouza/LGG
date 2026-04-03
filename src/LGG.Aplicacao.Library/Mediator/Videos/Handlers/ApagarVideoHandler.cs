using LGG.Aplicacao.Library.Mediator.Videos.Commands;
using LGG.Dominio.Library.Interfaces.IRepositorios.IVideo;
using MediatR;

namespace LGG.Aplicacao.Library.Mediator.Videos.Handlers
{
    public class ApagarVideoHandler : IRequestHandler<ApagarVideoCommand, Unit>
    {
        //private readonly IVideoRepositorio _videoRepositorio;

        public ApagarVideoHandler(/*IVideoRepositorio videoRepositorio*/)
        {
            //_videoRepositorio = videoRepositorio;
        }

        public async Task<Unit> Handle(ApagarVideoCommand request, CancellationToken cancellationToken)
        {
            //await _videoRepositorio.ApagarPorIdAsync(request.Id);
            return Unit.Value;
        }
    }
}
