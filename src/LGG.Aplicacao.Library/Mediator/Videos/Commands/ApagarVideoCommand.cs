using MediatR;

namespace LGG.Aplicacao.Library.Mediator.Videos.Commands
{
    public class ApagarVideoCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
