using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class SnippetChannelDto
    {
        [JsonPropertyName("publishedAt")]
        public DateTime PublishedAt { get; set; }

        [JsonPropertyName("channelId")]
        public string ChannelId { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("customUrl")]
        public string CustomUrl { get; set; } = string.Empty;

        [JsonPropertyName("thumbnails")]
        public ThumbnailsDto? Thumbnails { get; set; }
        
        [JsonPropertyName("defaultLanguage")]
        public string DefaultLanguage { get; set; } = string.Empty;

        [JsonPropertyName("localized")]
        public LocalizedDto? Localized { get; set; }
        
        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;
    }
}
