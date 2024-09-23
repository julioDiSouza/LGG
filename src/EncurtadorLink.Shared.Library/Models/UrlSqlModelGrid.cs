namespace EncurtadorLink.Shared.Library.Models
{
    public class UrlSqlModelGrid
    {
        private string _urlLonga = string.Empty;

        public Guid Id { get; set; }
        public string UrlLonga
        {
            get 
            { 
                return _urlLonga.Length > 115 ? 
                    _urlLonga.Substring(0, 110) + "..." : 
                    _urlLonga; 
            }
            set { _urlLonga = value; }
        }
        public string UrlCurta { get; set; } = string.Empty;


    }
}
