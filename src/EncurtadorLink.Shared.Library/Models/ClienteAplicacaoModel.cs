using EncurtadorLink.Entities.Library.Enumeradores;

namespace EncurtadorLink.Shared.Library.Models
{
    public class ClienteAplicacaoModel
    {
        public Guid ClienteId { get; set; }
        public virtual ClienteModel Cliente { get; set; }
        public Guid ClienteSecret { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ClienteAplicacaoTipoEnum Tipo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
