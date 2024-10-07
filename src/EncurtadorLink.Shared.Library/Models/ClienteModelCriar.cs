using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Shared.Library.Models
{
    public class ClienteModelCriar
    {
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;
        public string ReSenha { get; set; } = string.Empty;
    }
}
