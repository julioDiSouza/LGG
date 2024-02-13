namespace EncurtadorLink.Core.Library.Entidades.MySql
{
    public class UrlSql
    {
        public Guid Id { get; set; }
        public string UrlLonga { get; set; } = string.Empty;
        public string UrlCurta { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string IP { get; set; } = string.Empty;
        public DateTime DataInclusao { get; set; }
    }
}
