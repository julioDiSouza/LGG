namespace EncurtadorLink.Infra.Extensoes
{
    public static class ReplaceExtensao
    {
        public static string UrlSemHttp(this string url)
        {
            return url
                .Replace("https://", "")
                .Replace("http://", "");
        }
    }
}
