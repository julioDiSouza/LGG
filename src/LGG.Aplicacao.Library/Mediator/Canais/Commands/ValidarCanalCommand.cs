using MediatR;

namespace LGG.Aplicacao.Library.Mediator.Canais.Commands
{
    public class ValidarCanalCommand : IRequest<bool>
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;

        public ValidarCanalCommand(string id, string nome)
        {
            UsuarioId = id;
            Nome = nome;
        }
    }
}