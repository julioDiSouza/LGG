namespace LGG.Aplicacao.Library.Dtos
{
    public class VideoConsultaDto
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }
        public Guid CanalId { get; set; }
        public long QtdView { get; set; }
        public long QtdLike { get; set; }
        public string UrlImagem { get; set; }
        public DateTime DataPublicacao { get; set; }
    }
}
