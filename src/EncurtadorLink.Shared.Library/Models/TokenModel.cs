namespace EncurtadorLink.Shared.Library.Models
{
    public interface ITokenModel { }

    public record TokenBuilder (int Status, string Detail) { }

    public class TokenModel : ITokenModel
    {
        public TokenModel() { }

        public TokenModel(string accessToken, int expiresIn)
        {
            Status = 200;
            TokenType = "Bearer";
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
        }

        public int Status { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; } = string.Empty;
    }

    public class RefreshTokenModel : ITokenModel
    {
        public RefreshTokenModel() { }

        public RefreshTokenModel(string accessToken, string refreshToken)
        {
            Status = 200;
            TokenType = "Bearer";
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public int Status { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = string.Empty;
    }

    public class TokenErroModel : ITokenModel
    {
        public int Status { get; set; }
        public string Erro { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
    }
}