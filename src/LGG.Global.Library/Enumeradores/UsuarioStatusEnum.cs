using System.ComponentModel;

namespace LGG.Global.Library.Enumeradores
{
    public enum UsuarioStatusEnum
    {
        [Description("Pendente")]
        Pendente,
        [Description("Ativo")]
        Ativo,
        [Description("Bloquado")]
        Bloqueado,
        [Description("Excluído")]
        Excluido
    }
}
