using EncurtadorLink.Entidades.Enum;
using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Model.Usuario
{
    public class ClienteDtoCriar
    {
        public string RazaoSocial { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Documento { get; set; }
        public DocumentoTipo DocumentoTipo { get; set; }
        public string Senha { get; set; }
        [Compare(nameof(Senha), ErrorMessage = "A confirmação de senha não é válida.")]
        public string ReSenha { get; set; }
    }
}
