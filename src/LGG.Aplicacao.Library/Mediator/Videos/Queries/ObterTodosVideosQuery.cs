using LGG.Aplicacao.Library.Dtos;
using MediatR;

namespace LGG.Aplicacao.Library.Mediator.Videos.Queries
{
    public class ObterTodosVideosQuery : IRequest<List<VideoConsultaDto>>
    {
    }
}
