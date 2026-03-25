using LGG.Global.Library.Extensoes;
using LGG.Global.Library.Enumeradores;

namespace LGG.Global.Library.Erros
{
    public class Erro
    {
        public Erro(MsgSistemaEnum erroEnum)
        {
            Codigo = erroEnum;
            Mensagem = erroEnum.ObterDescricao();
        }

        public MsgSistemaEnum Codigo { get; set; }
        public string Mensagem { get; set; } = string.Empty;
    }
}
