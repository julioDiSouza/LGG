using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Entities.Library.Sql
{
    public class ClienteAplicacao
    {
        [Required]
        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        [Required]
        public Guid ClienteSecret { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public bool Ativo { get; set; }
        [Required]
        public DateTime DataInclusao { get; set; }
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
