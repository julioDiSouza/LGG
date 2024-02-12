namespace EncurtadorLink.Services.Library.Models
{
    public class UrlMongoDbModel
    {
        public string Id { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string UrlLonga { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
