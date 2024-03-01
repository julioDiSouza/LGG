namespace EncurtadorLink.Entities.Library.Mongo
{
    public class UrlMongoDb : EntidadeId
    {
        public string Codigo { get; set; } = string.Empty;
        public string UrlLonga { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
