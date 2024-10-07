using System.ComponentModel.DataAnnotations;
using EncurtadorLink.Entities.Library.Enumeradores;

namespace EncurtadorLink.Entities.Library.Sql
{
    public class FilaEmail
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        [Required]
        public string Email { get; set; }
        public string? ConfirmationToken { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordDataExpiracao { get; set; }
        public DateTime? DataEnvio { get; set; }
        public int Tipo { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
