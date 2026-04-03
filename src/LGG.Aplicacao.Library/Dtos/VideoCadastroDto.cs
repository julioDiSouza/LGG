namespace LGG.Aplicacao.Library.Dtos
{
    public class VideoCadastroDto
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }
        public Guid CanalId { get; set; }
    }
}
