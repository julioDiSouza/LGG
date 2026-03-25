using MediatR;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Queries
{
    public class ObterUsuarioQuery : IRequest<UsuarioConsultaDto>
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}