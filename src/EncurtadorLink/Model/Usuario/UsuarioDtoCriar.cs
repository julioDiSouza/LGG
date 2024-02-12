using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Model.Usuario
{
    public class UsuarioDtoCriar
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid ClienteId { get; set; }
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;
        [Compare(nameof(Senha), ErrorMessage = "A confirmação de senha não é válida.")]
        public string ReSenha { get; set; } = string.Empty;
    }
}
