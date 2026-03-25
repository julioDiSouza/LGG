using LGG.Aplicacao.Library.Dtos.Base;

namespace LGG.Aplicacao.Library.Dtos
{
    public class CanalValidacaoDtoMsg : MsgBaseDto
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;

        public CanalValidacaoDtoMsg() { }
        public CanalValidacaoDtoMsg(string cryptoKeyFila) : base(cryptoKeyFila) { }
    }
}