using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Aplicacao.Library.Helpers;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.Usuarios.Commands;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Handlers
{
    public class AtualizarUsuarioHandler : IRequestHandler<AtualizarUsuarioCommand, UsuarioConsultaDto>
    {
        private readonly IUnidTrabalho _ut;

        public AtualizarUsuarioHandler(IUnidTrabalho ut)
        {
            _ut = ut;
        }

        public async Task<UsuarioConsultaDto> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.UsuarioId, out Guid usuarioId))
                return null;

            var usuario = await _ut.UsuarioRepositorio.ObterPorIdAsync(usuarioId);

            if (usuario != null)
                usuario = await _ut.UsuarioRepositorio.AtualizarAsync(usuarioId, request.Nome, request.Sexo, request.DataNascimento);

            return usuario!.ConverterParaUsuarioConsultaDto();
        }
    }
}
