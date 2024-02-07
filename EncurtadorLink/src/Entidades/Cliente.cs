using EncurtadorLink.Entidades.Enum;

namespace EncurtadorLink.Entidades
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public DocumentoTipo DocumentoTipo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }

        public virtual ICollection<ClienteAplicacao> Aplicacoes { get; set; }
    }
}