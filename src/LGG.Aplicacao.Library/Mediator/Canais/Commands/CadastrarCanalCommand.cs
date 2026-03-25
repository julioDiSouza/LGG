using MediatR;

namespace LGG.Aplicacao.Library.Mediator.Canais.Commands
{
    public class CadastrarCanalCommand : IRequest<bool>
    {
        public Guid UsuarioId { get; set; }

        public CadastrarCanalCommand(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}
