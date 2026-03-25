using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Enumeradores;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Commands
{
    public class AtualizarUsuarioCommand : IRequest<UsuarioConsultaDto>
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public UsuarioSexoEnum Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}