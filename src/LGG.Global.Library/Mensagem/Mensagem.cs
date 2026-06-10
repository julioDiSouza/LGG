using LGG.Global.Library.Extensoes;
using LGG.Global.Library.Enumeradores;

namespace LGG.Global.Library.Mensagem
{
    public class Mensagem
    {
        public bool Sucesso { get; set; }
        public SistemaEnum Codigo { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public Mensagem() 
        {
            Sucesso = false;
        }

        public Mensagem(SistemaEnum msgEnum)
        {
            Codigo = msgEnum;
            Descricao = msgEnum.ObterDescricao();
            Sucesso = VerificarSucesso(msgEnum);
        }

        private bool VerificarSucesso(SistemaEnum msgEnum)
        {
            switch (msgEnum)
            {
                case SistemaEnum.ProcessadoSucesso:
                case SistemaEnum.ApiFuncionando:
                    return true;
                default:
                    return false;
            }
        }
    }
}
