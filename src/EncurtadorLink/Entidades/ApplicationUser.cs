using Microsoft.AspNetCore.Identity;

namespace EncurtadorLink.Entidades
{
    public class ApplicationUser : IdentityUser
    {
        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
