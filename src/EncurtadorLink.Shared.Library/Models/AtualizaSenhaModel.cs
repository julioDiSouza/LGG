namespace EncurtadorLink.Shared.Library.Models
{
    public class AtualizaSenhaModel
    {
        public string Email { get; set; }
        public string EmailToken { get; set; }
        public string NovaSenha { get; set; }
        public string ReNovaSenha { get; set; }
    }
}