using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Entities.Library.Sql
{
    public class Cliente
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string NomeCompleto { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public bool Ativo { get; set; }
        [Required]
        public DateTime DataInclusao { get; set; }
        
        public virtual ICollection<ClienteAplicacao> Aplicacoes { get; set; }
        public virtual ICollection<UrlSql> Urls { get; set; }
        public virtual ICollection<FilaEmail> FilaEmails { get; set; }
    }
}
