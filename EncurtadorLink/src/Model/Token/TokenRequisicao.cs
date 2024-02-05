using Newtonsoft.Json;

namespace EncurtadorLink.Model.Token
{
    public class TokenRequisicao
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; } = string.Empty;
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; } = string.Empty;   
    }
}