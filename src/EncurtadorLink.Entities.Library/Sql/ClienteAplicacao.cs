using EncurtadorLink.Entities.Library.Enumeradores;

namespace EncurtadorLink.Entities.Library.Sql
{
    public class ClienteAplicacao
    {
        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public Guid ClienteSecret { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ClienteAplicacaoTipoEnum Tipo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
