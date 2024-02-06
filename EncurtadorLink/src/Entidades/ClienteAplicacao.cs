using EncurtadorLink.Entidades.Enum;
using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Entidades
{
    public class ClienteAplicacao
    {
        [Required(ErrorMessage = "ClienteId: Campo obrigatório.")]
        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        [Required(ErrorMessage = "ClienteSecret: Campo obrigatório.")]
        public Guid ClienteSecret { get; set; }
        [Required(ErrorMessage = "Nome: Campo obrigatório.")]
        public string Nome { get; set; } = string.Empty;
        public ClienteAplicacaoTipo Tipo { get; set; }
        public bool Ativo { get; set; }
        [Required(ErrorMessage = "DataInclusao: Campo obrigatório.")]
        public DateTime DataInclusao { get; set; }
    }
}