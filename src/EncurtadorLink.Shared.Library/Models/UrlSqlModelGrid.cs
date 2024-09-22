namespace EncurtadorLink.Shared.Library.Models
{
    public class UrlSqlModelGrid
    {
        public Guid Id { get; set; }
        public string UrlLonga { get; set; } = string.Empty;
        public string UrlCurta { get; set; } = string.Empty;
    }
}
