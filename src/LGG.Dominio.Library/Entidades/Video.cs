using LGG.Global.Library.Enumeradores;

namespace LGG.Dominio.Library.Entidades
{
    public class Video
    {
        public long Id { get; set; }
        public Guid UsuarioId { get; set; }
        public int CampeonatoMensalId { get; set; }
        public long VideoCicloId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string VideoId { get; set; } = string.Empty;
        public long QtdView { get; set; }
        public long QtdLike { get; set; }
        public string UrlImagem { get; set; } = string.Empty;
        public DateTime DataPublicacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public VideoStatusEnum Status { get; set; }
    }
}
