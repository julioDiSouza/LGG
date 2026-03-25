using MediatR;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Queries
{
    public class ObterUsuarioTokenQuery : IRequest<UsuarioTokenRespostaDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
