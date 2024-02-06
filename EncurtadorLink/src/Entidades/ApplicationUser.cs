using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Entidades
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "ClienteId: Campo obrigatório.")]
        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        [Required(ErrorMessage = "NomeCompleto: Campo obrigatório.")]
        public string NomeCompleto { get; set; }
    }
}
