using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Model.Usuario
{
    public class UsuarioDtoCriar
    {
        public string NomeUsuario { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public Guid ClienteId { get; set; }
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Compare(nameof(Senha), ErrorMessage = "A confirmação de senha não é válida.")]
        public string ReSenha { get; set; }
    }
}
