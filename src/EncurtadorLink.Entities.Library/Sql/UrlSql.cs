using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Entities.Library.Sql
{
    public class UrlSql
    {
        [Required]
        public Guid Id { get; set; }
        public Guid? ClienteId { get; set; }
        [Required]
        public string UrlLonga { get; set; } = string.Empty;
        [Required]
        public string UrlCurta { get; set; } = string.Empty;
        [Required]
        public string Codigo { get; set; } = string.Empty;
        [Required]
        public string IP { get; set; } = string.Empty;
        [Required]
        public DateTime DataInclusao { get; set; }
    }
}
