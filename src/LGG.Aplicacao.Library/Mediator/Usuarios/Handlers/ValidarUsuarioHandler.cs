using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Enumeradores;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.Usuarios.Queries;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Handlers
{
    public class ValidarUsuarioHandler : IRequestHandler<ValidarUsuarioQuery, (bool, UsuarioTokenRespostaDto?)>
    {
        private readonly IMediator _mediator;
        private readonly IUnidTrabalho _ut;

        public ValidarUsuarioHandler(IUnidTrabalho ut, IMediator mediator)
        {
            _mediator = mediator;
            _ut = ut;
        }

        public async Task<(bool, UsuarioTokenRespostaDto?)> Handle(ValidarUsuarioQuery request, CancellationToken cancellationToken)
        {
            var usuarioId = new Guid(request.Id);

            var usuario = await _ut.UsuarioRepositorio.ObterPorIdAsync(usuarioId);
            if (usuario == null ||
                usuario.Email != request.Email ||
                usuario.Status != UsuarioStatusEnum.Pendente)
                return (false, null);

            await _ut.UsuarioRepositorio.AtualizarStatusAsync(usuarioId, UsuarioStatusEnum.Ativo);

            var token = await _mediator.Send(
                new ObterTokenQuery { UsuarioId = usuario.Id.ToString(), Email = usuario.Email, Nome = usuario.Nome });

            return (true, token);
        }
    }
}
