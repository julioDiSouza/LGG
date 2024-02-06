using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Model.Usuario
{
    public class UsuarioDtoCriar
    {
        [Required(ErrorMessage = "NomeUsuario: Campo obrigatório.")]
        public string NomeUsuario { get; set; }
        [Required(ErrorMessage = "NomeCompleto: Campo obrigatório.")]
        public string NomeCompleto { get; set; }
        [Required(ErrorMessage = "Email: Campo obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "ClienteId: Campo obrigatório.")]
        public Guid ClienteId { get; set; }
        [Required(ErrorMessage = "Senha: Campo obrigatório.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Required(ErrorMessage = "ReSenha: Campo obrigatório.")]
        [Compare("Password")]
        public string ReSenha { get; set; }
    }
}
