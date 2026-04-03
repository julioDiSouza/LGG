using LGG.Global.Library.Extensoes;
using LGG.Global.Library.Enumeradores;

namespace LGG.Global.Library.Erros
{
    public class Erro
    {
        public Erro(SistemaEnum erroEnum)
        {
            Codigo = erroEnum;
            Mensagem = erroEnum.ObterDescricao();
        }

        public SistemaEnum Codigo { get; set; }
        public string Mensagem { get; set; } = string.Empty;
    }
}
