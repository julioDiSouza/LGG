using LGG.Aplicacao.Library.Dtos;
using MediatR;

namespace LGG.Aplicacao.Library.Mediator.Videos.Commands
{
    public class CadastrarVideoCommand : IRequest<VideoCadastroRetornoDto>
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }
        public Guid CanalId { get; set; }
    }
}
