namespace EncurtadorLink.Core.Library.Entidades.MySql
{
    public class UrlEncurtada
    {
        public Guid Id { get; set; }
        public string UrlLonga { get; set; } = string.Empty;
        public string UrlCurta { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime DataInclusaoUtc { get; set; }
    }
}
