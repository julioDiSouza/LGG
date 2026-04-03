using MediatR;
using LGG.Global.Library.Enumeradores;

namespace LGG.Aplicacao.Library.Mediator.Videos.Commands
{
    public class PublicarVideoCommand : IRequest<SistemaEnum>
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;

        public PublicarVideoCommand(string usuarioId, string link)
        {
            UsuarioId = usuarioId;
            Link = link;
        }
    }
}
