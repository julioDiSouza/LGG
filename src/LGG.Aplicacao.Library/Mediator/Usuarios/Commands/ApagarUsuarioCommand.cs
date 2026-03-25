using MediatR;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Commands
{
    public class ApagarUsuarioCommand : IRequest<bool>
    {
        public string UsuarioId { get; set; } = string.Empty;
    }
}
