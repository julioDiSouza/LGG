namespace LGG.Aplicacao.Library.Dtos
{
    public class VideoTopDezDto
    {
        public long VideoId { get; set; }
        public Guid UsuarioId { get; set; }
        public string UsuarioNome { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public long QtdView { get; set; }
        public long QtdLike { get; set; }
        public string UrlImagem { get; set; } = string.Empty;
        public int Posicao { get; set; }
        public bool Destacar { get; set; }
    }
}
