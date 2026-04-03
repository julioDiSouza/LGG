using LGG.Global.Library.Erros;

namespace LGG.Aplicacao.Library.Dtos
{
    public class VideoCadastroRetornoDto : ErroDto
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Url { get; set; }
        public Guid CanalId { get; set; }
    }
}
