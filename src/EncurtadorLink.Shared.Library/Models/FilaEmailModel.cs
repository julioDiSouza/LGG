using EncurtadorLink.Entities.Library.Sql;
using EncurtadorLink.Shared.Library.Enumeradores;

namespace EncurtadorLink.Shared.Library.Models
{
    public class FilaEmailModel
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }
        public string ResetPasswordToken { get; set; }
        public DateTime ResetPasswordDataExpiracao { get; set; }
        public DateTime? DataEnvio { get; set; }
        public FilaEmailEnum Tipo { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}