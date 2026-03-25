using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class ChannelDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("keywords")]
        public string Keywords { get; set; } = string.Empty;

        [JsonPropertyName("trackingAnalyticsAccountId")]
        public string TrackingAnalyticsAccountId { get; set; } = string.Empty;

        [JsonPropertyName("unsubscribedTrailer")]
        public string UnsubscribedTrailer { get; set; } = string.Empty;

        [JsonPropertyName("defaultLanguage")]
        public string DefaultLanguage { get; set; } = string.Empty;

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;
    }
}
