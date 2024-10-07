using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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
        public string Role { get; set; } = string.Empty;
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenDataExpiracao { get; set; }
        public string? ResetPasswordToken { get; set; } = string.Empty;
        public DateTime? ResetPasswordDataExpiracao { get; set; }
    }
}
