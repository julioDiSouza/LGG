using EncurtadorLink.Shared.Library.Enumeradores;
using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Shared.Library.Models
{
    public class ClienteModelCriar
    {
        public string RazaoSocial { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public DocumentoTipoEnum DocumentoTipo { get; set; }
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;
        public string ReSenha { get; set; } = string.Empty;
    }
}
