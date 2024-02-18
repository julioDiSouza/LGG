namespace EncurtadorLink.Shared.Library.Models
{
    public interface ITokenModel { }

    public class TokenModel : ITokenModel
    {
        public string AccessToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; } = string.Empty;
    }

    public class TokenErroModel : ITokenModel
    {
        public string Erro { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}
