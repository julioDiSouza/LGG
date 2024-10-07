namespace EncurtadorLink.Shared.Library.Models
{
    public class ClienteAplicacaoModel
    {
        public Guid ClienteId { get; set; }
        public virtual ClienteModel Cliente { get; set; }
        public Guid ClienteSecret { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
        public string TipoRole { get; set; }
    }
}
