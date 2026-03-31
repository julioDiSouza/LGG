using LGG.Aplicacao.Library.Dtos.Base;

namespace LGG.Aplicacao.Library.Dtos
{
    public class VideoPublicacaoDtoMsg : MsgBaseDto
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;

        public VideoPublicacaoDtoMsg() { }
        public VideoPublicacaoDtoMsg(string cryptoKeyFila) : base(cryptoKeyFila) { }
    }
}
