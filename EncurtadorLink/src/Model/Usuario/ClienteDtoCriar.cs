using EncurtadorLink.Entidades.Enum;
using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Model.Usuario
{
    public class ClienteDtoCriar
    {
        public string RazaoSocial { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public DocumentoTipo DocumentoTipo { get; set; }
        public string Senha { get; set; } = string.Empty;
        [Compare(nameof(Senha), ErrorMessage = "A confirmação de senha não é válida.")]
        public string ReSenha { get; set; } = string.Empty;
    }
}
