namespace LGG.Aplicacao.Library.Dtos
{
    public class TopDezDto : MensagemDto
    {
        public long VideoCicloId { get; set; }
        public int CampeonatoMensalId { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public List<VideoTopDezDto> Videos { get; set; } = new List<VideoTopDezDto>();
    }
}
