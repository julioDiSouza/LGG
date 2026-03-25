using MediatR;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Queries
{
    public class ObterTokenQuery : IRequest<UsuarioTokenRespostaDto>
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
