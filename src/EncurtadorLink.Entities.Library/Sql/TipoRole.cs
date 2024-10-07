using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Entities.Library.Sql
{
    public class TipoRole
    {
        [Required]
        public string Role { get; set; } = string.Empty;

        public virtual ICollection<ApplicationUser> Usuarios { get; set; }
        public virtual ICollection<ClienteAplicacao> Aplicacoes { get; set; }
    }
}