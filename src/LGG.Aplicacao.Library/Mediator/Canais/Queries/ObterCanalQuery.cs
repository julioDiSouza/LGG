using MediatR;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Mediator.Canais.Queries
{
    public class ObterCanalQuery : IRequest<CanalDto>
    {
        public string CanalId { get; set; } = string.Empty;
    }
}
