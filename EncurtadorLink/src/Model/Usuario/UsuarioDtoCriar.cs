using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Model.Usuario
{
    public class UsuarioDtoCriar
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Required]
        [Compare("Password")]
        public string ReSenha { get; set; }
    }
}
