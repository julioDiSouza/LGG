using System.ComponentModel;

namespace LGG.Global.Library.Enumeradores
{
    public enum VideoCicloStatusEnum
    {
        [Description("Ativo")]
        Ativo,
        [Description("Processamento")]
        Processamento,
        [Description("Concluido")]
        Concluido
    }
}