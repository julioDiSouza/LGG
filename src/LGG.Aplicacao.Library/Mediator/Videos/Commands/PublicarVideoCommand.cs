using MediatR;

namespace LGG.Aplicacao.Library.Mediator.Videos.Commands
{
    public class PublicarVideoCommand : IRequest<bool>
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
