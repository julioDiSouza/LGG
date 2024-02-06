using System.ComponentModel.DataAnnotations;

namespace EncurtadorLink.Model.Requisicao
{
    public class UrlRequisicaoModel
    {
        [Required(ErrorMessage = "Url: Campo obrigatório.")]
        public string Url { get; set; } = string.Empty;
    }
}
