using MediatR;
using LGG.Aplicacao.Library.Dtos;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Queries
{
    public class ObterTodosUsuariosQuery : IRequest<List<UsuarioConsultaDto>>
    {
    }
}