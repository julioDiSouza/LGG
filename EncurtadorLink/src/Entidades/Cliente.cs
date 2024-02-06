using EncurtadorLink.Entidades.Enum;
using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Entidades
{
    public class Cliente
    {
        [Required(ErrorMessage = "Id: Campo obrigatório.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nome: Campo obrigatório.")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email: Campo obrigatório.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Documento: Campo obrigatório.")]
        public string Documento { get; set; } = string.Empty;
        public DocumentoTipo DocumentoTipo { get; set; }
        public bool Ativo { get; set; }
        [Required(ErrorMessage = "DataInclusao: Campo obrigatório.")]
        public DateTime DataInclusao { get; set; }

        public virtual ICollection<ClienteAplicacao> Aplicacoes { get; set; }
    }
}