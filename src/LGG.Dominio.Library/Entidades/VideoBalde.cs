namespace LGG.Dominio.Library.Entidades
{
    public class VideoBalde
    {
        public long Id { get; set; }
        public long VideoCicloId { get; set; }
        public Guid UsuarioId { get; set; }
        public long VideoId { get; set; }
        public string VideoIdYt { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public DateTime DataInicioProcessamento { get; set; }
    }
}
