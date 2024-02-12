using EncurtadorLink.Entidades.Enum;

namespace EncurtadorLink.Entidades
{
    public class ClienteAplicacao
    {
        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public Guid ClienteSecret { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ClienteAplicacaoTipo Tipo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}