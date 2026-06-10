namespace LGG.Aplicacao.Library.Dtos
{
    public class VideoCadastroRetornoDto : MensagemDto
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Url { get; set; }
        public Guid CanalId { get; set; }
    }
}
