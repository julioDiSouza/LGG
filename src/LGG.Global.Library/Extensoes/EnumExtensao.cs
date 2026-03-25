using System.ComponentModel;

namespace LGG.Global.Library.Extensoes
{
    public static class EnumExtensao
    {
        public static string ObterDescricao(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)field!
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault()!;

            return attribute?.Description ?? value.ToString();
        }
    }
}
