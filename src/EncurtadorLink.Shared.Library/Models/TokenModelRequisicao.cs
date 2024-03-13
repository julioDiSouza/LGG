using Newtonsoft.Json;

namespace EncurtadorLink.Shared.Library.Models
{
    public class TokenModelRequisicao
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; } = string.Empty;
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; } = string.Empty;
    }
}
