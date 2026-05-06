using LGG.Aplicacao.Library.Dtos;
using MediatR;

namespace LGG.Aplicacao.Library.Mediator.Videos.Commands
{
    public class AtualizarVideoCommand : IRequest<VideoAtualizarDto>
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }
    }
}
