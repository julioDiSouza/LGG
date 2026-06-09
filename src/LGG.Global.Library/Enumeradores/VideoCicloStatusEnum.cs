using System.ComponentModel;

namespace LGG.Global.Library.Enumeradores
{
    public enum VideoCicloStatusEnum
    {
        [Description("Processando Balde")]
        ProcessandoBalde,
        [Description("Processando Métricas")]
        ProcessandoMetricas,
        [Description("Processando Top 10")]
        ProcessandoTopDez,
        [Description("Concluído")]
        Concluido
    }
}