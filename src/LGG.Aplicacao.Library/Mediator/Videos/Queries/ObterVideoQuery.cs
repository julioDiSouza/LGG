using LGG.Aplicacao.Library.Dtos;
using MediatR;

namespace LGG.Aplicacao.Library.Mediator.Videos.Queries
{
    public class ObterVideoQuery : IRequest<VideoConsultaDto>
    {
        public long Id { get; set; }
    }
}
