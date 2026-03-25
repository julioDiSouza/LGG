using System.ComponentModel;

namespace LGG.Global.Library.Enumeradores
{
    public enum CanalStatusEnum
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
        [Description("Bloqueado por quantidade de inscritos")]
        BloqueadoPorQuantidadeInscritos,
        [Description("Erro na Validação")]
        ErroValidacao
    }
}
