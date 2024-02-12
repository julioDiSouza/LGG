using Newtonsoft.Json;

namespace EncurtadorLink.Services.Library.Models
{
    public interface ITokenModel { }

    public class TokenModel : ITokenModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; } = string.Empty;
    }

    public class TokenErroModel : ITokenModel
    {
        [JsonProperty("error")]
        public string Erro { get; set; } = string.Empty;
        [JsonProperty("error_description")]
        public string Descricao { get; set; } = string.Empty;
    }
}
