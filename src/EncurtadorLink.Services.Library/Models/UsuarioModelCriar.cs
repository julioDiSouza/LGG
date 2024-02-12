using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Services.Library.Models
{
    public class UsuarioModelCriar
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid ClienteId { get; set; }
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;
        public string ReSenha { get; set; } = string.Empty;
    }
}
