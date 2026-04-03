namespace LGG.Dominio.Library.Entidades
{
    public class Video
    {
        public long Id { get; set; }
        public int CampeonatoMensalId { get; set; }
        public long VideoCicloId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid CanalId { get; set; }
        public string VideoId { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string UrlImagem { get; set; } = string.Empty;
        public long QtdView { get; set; }
        public long QtdLike { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataPublicacao { get; set; }
        public bool Ativo { get; set; }

        public Video Cadastrar(string titulo, string descricao, string url, string videoId, Guid canalId)
        {
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Url = url;
            this.VideoId = videoId;
            this.CanalId = canalId;
            this.QtdView = 0;
            this.QtdLike = 0;
            this.DataCadastro = DateTime.UtcNow;
            this.DataPublicacao = DateTime.UtcNow;
            this.Ativo = true;

            return this;
        }

        public void Atualizar(string titulo, string descricao, string url)
        {
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Url = url;
        }
    }
}
