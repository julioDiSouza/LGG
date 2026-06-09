namespace LGG.Dominio.Library.Entidades
{
    public class VideoTopDez
    {
        public long Id { get; set; }
        public long VideoId { get; set; }
        public long VideoCicloId { get; set; }
        public Guid UsuarioId { get; set; }
        public int CampeonatoMensalId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public long QtdView { get; set; }
        public long QtdLike { get; set; }
        public string UrlImagem { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
    }
}
