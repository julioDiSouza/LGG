using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncurtadorLink.Entities.Library.Sql
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        [Required]
        public string NomeCompleto { get; set; } = string.Empty;
        [Required]
        public bool Ativo { get; set; }
        [Required]
        public DateTime DataInclusao { get; set; }
        [Required]
        [ForeignKey("Role")]
        public string TipoRole { get; set; } = string.Empty;

        public virtual Role Role { get; set; }
    }
}
