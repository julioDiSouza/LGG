namespace EncurtadorLink.Core.Library.Entidades.MongoDb
{
    public class UrlMongoDB : EntidadeId
    {
        public string Codigo { get; set; } = string.Empty;
        public string UrlLonga { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
