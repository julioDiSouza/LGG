namespace LGG.Dominio.Library.Entidades
{
    public class VideoEstatistica
    {
        public long Id { get; set; }
        public long VideoCicloId { get; set; }
        public long VideoId { get; set; }
        public int QtdView { get; set; }
        public int QtdLike { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
