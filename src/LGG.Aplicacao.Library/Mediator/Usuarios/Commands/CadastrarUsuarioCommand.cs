using MediatR;
using LGG.Aplicacao.Library.Dtos;
using LGG.Global.Library.Enumeradores;

namespace LGG.Aplicacao.Library.Mediator.Usuarios.Commands
{
    public class CadastrarUsuarioCommand : IRequest<UsuarioCadastroRetornoDto>
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UsuarioSexoEnum Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public UsuarioTipoEnum Tipo { get; set; }
        public UsuarioStatusEnum Status { get; set; }
        public string Senha { get; set; } = string.Empty;
    }
}
