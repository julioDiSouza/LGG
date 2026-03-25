using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Aplicacao.Library.Helpers;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.Usuarios.Queries;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Handlers
{
    public class ObterTodosUsuariosHandler : IRequestHandler<ObterTodosUsuariosQuery, List<UsuarioConsultaDto>>
    {
        private readonly IUnidTrabalho _ut;

        public ObterTodosUsuariosHandler(IUnidTrabalho ut)
        {
            _ut = ut;
        }

        public async Task<List<UsuarioConsultaDto>> Handle(ObterTodosUsuariosQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _ut.UsuarioRepositorio.ObterTodosAsync();

            return usuarios.ConverterParaUsuariosConsultaDto();
        }
    }
}
