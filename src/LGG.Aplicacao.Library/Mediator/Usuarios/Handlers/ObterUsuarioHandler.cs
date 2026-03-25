using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Aplicacao.Library.Helpers;
using LGG.Dominio.Library.Entidades;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Aplicacao.Library.Mediator.Usuarios.Queries;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Handlers
{
    public class ObterUsuarioHandler : IRequestHandler<ObterUsuarioQuery, UsuarioConsultaDto?>
    {
        private readonly IUnidTrabalho _ut;

        public ObterUsuarioHandler(IUnidTrabalho ut)
        {
            _ut = ut;
        }

        public async Task<UsuarioConsultaDto?> Handle(ObterUsuarioQuery request, CancellationToken cancellationToken)
        {
            Usuario? usuario = null;

            if (string.IsNullOrEmpty(request.Id) && 
                !string.IsNullOrEmpty(request.Email) && 
                !string.IsNullOrEmpty(request.Senha))
            {
                var usuarioPorSenha = await _ut.UsuarioRepositorio.ObterPorEmailSenhaAsync(request.Email, request.Senha);
                if (usuarioPorSenha != null && BCrypt.Net.BCrypt.Verify(request.Senha, usuarioPorSenha.Senha))
                    usuario = usuarioPorSenha; 
            }
            else
            {
                if (!Guid.TryParse(request.Id, out Guid usuarioId))
                    return null;

                usuario = await _ut.UsuarioRepositorio.ObterPorIdAsync(usuarioId);
            }

            return usuario?.ConverterParaUsuarioConsultaDto();
        }
    }
}
