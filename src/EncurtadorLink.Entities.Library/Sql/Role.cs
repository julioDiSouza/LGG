using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Entities.Library.Sql
{
    public class Role
    {
        [Required]
        public string TipoRole { get; set; } = string.Empty;

        public virtual ICollection<ApplicationUser> Usuarios { get; set; }
        public virtual ICollection<ClienteAplicacao> Aplicacoes { get; set; }
    }
}