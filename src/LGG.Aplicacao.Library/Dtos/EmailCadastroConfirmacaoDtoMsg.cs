using LGG.Aplicacao.Library.Dtos.Base;

namespace LGG.Aplicacao.Library.Dtos
{
    public class EmailCadastroConfirmacaoDtoMsg : MsgBaseDto
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public EmailCadastroConfirmacaoDtoMsg() { }
        public EmailCadastroConfirmacaoDtoMsg(string cryptoKeyFila) : base(cryptoKeyFila) { } 
    }
}
