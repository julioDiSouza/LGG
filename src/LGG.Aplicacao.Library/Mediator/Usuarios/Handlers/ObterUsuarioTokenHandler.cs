using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Aplicacao.Library.Mediator.Usuarios.Queries;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Handlers
{
    public class ObterUsuarioTokenHandler : IRequestHandler<ObterUsuarioTokenQuery, UsuarioTokenRespostaDto?>
    {
        private readonly IMediator _mediator;

        public ObterUsuarioTokenHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<UsuarioTokenRespostaDto?> Handle(ObterUsuarioTokenQuery request, CancellationToken cancellationToken)
        {
            UsuarioTokenRespostaDto? token = null;

            var usuario = await _mediator.Send(new ObterUsuarioQuery { Email = request.Email, Senha = request.Senha });
            if (usuario != null)
            {
                token = await _mediator.Send(new ObterTokenQuery
                {
                    UsuarioId = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email
                });
            }

            return token;
        }
    }
}
