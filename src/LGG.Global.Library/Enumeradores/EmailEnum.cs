using System.ComponentModel;

namespace LGG.Global.Library.Enumeradores
{
    public enum EmailEnum
    {
        [Description("Confirmação de email")]
        Confirmacao,
        [Description("Recuperação de senha")]
        RecuperacaoSenha,
        [Description("Campeão mensal")]
        CampeaoMensal
    }
}
