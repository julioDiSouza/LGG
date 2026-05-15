using System.ComponentModel;

namespace LGG.Global.Library.Enumeradores
{
    public enum VideoCicloStatusEnum
    {
        [Description("Processando Balde")]
        ProcessandoBalde,
        [Description("Processando Métricas")]
        ProcessandoMetricas,
        [Description("Concluído")]
        Concluido
    }
}