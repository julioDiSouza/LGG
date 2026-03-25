namespace LGG.Global.Library.Extensoes
{
    public static class HttpExtensao
    {
        public static string AjustarUrl(this string url)
            => !string.IsNullOrEmpty(url) && url.EndsWith("/") ? url : url + "/";

        public static string AjustarNome(this string nome)
            => !string.IsNullOrEmpty(nome) && nome.StartsWith("@") ? nome : "@" + nome;
    }
}
