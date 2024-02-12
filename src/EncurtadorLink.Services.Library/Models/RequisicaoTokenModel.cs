using Newtonsoft.Json;

namespace EncurtadorLink.Services.Library.Models
{
    public class RequisicaoTokenModel
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; } = string.Empty;
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; } = string.Empty;
    }
}
