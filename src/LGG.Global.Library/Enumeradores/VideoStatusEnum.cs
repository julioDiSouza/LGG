using System.ComponentModel;

namespace LGG.Global.Library.Enumeradores
{
    public enum VideoStatusEnum
    {
        [Description("Pendente")]
        Pendente,
        [Description("Ativo")]
        Ativo,
        [Description("Bloquado")]
        Bloqueado,
        [Description("Excluído")]
        Excluido,
        [Description("Não Localizado")]
        NaoLocalizado,
        [Description("Erro na Validação")]
        ErroValidacao
    }
}
