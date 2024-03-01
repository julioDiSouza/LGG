using EncurtadorLink.Entities.Library.Enumeradores;
using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Entities.Library.Sql
{
    public class Cliente
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Documento { get; set; } = string.Empty;
        [Required]
        public DocumentoTipoEnum DocumentoTipo { get; set; }
        [Required]
        public bool Ativo { get; set; }
        [Required]
        public DateTime DataInclusao { get; set; }

        public virtual ICollection<ClienteAplicacao> Aplicacoes { get; set; }
    }
}
