using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Entidades
{
    public class UrlEncurtada
    {
        [Required(ErrorMessage = "Guid: Campo obrigatório.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "UrlLonga: Campo obrigatório.")]
        public string UrlLonga { get; set; } = string.Empty;
        [Required(ErrorMessage = "UrlCurta: Campo obrigatório.")]
        public string UrlCurta { get; set; } = string.Empty;
        [Required(ErrorMessage = "Codigo: Campo obrigatório.")]
        public string Codigo { get; set; } = string.Empty;
        [Required(ErrorMessage = "DataInclusaoUtc: Campo obrigatório.")]
        public DateTime DataInclusaoUtc { get; set; }
    }
}
