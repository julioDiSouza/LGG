namespace EncurtadorLink.Entidades
{
    public class UrlEncurtada
    {
        public Guid Id { get; set; }
        public string UrlLonga { get; set; } = string.Empty;
        public string UrlCurta { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public DateTime DataInclusaoUtc { get; set; }
    }
}
