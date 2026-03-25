using MediatR;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.Usuarios.Commands;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Handlers
{
    public class ApagarUsuarioHandler : IRequestHandler<ApagarUsuarioCommand, bool>
    {
        private readonly IUnidTrabalho _ut;

        public ApagarUsuarioHandler(IUnidTrabalho ut)
        {
            _ut = ut;
        }

        public async Task<bool> Handle(ApagarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.UsuarioId, out Guid usuarioId))
                return false;

            await _ut.UsuarioRepositorio.ApagarPorIdAsync(usuarioId);

            return true;
        }
    }
}
