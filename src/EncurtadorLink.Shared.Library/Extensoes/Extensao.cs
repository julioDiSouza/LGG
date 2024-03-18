using EncurtadorLink.Entities.Library.Enumeradores;

namespace EncurtadorLink.Shared.Library.Extensoes
{
    public static class Extensao
    {
        public static string UrlSemHttp(this string url)
        {
            if (url is null)
                return url;

            return url
                .Replace("https://", "")
                .Replace("http://", "");
        }

        public static string ObterUserName(this string email)
        {
            if (email is null)
                return email;

            var arr = email.Split('@');
            return arr[0];
        }

        public static RoleEnum ConverterParaRole(this string texto)
        {
            return (RoleEnum)Enum.Parse(typeof(RoleEnum), texto);
        }
    }
}
