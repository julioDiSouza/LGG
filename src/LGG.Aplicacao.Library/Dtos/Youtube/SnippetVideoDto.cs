using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class SnippetVideoDto
    {
        [JsonPropertyName("publishedAt")]
        public DateTime PublishedAt { get; set; }

        [JsonPropertyName("channelId")]
        public string ChannelId { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("thumbnails")]
        public ThumbnailsDto? Thumbnails { get; set; }

        [JsonPropertyName("channelTitle")]
        public string ChannelTitle { get; set; } = string.Empty;

        [JsonPropertyName("tags")]
        public List<string>? Tags { get; set; }

        [JsonPropertyName("categoryId")]
        public string CategoryId { get; set; } = string.Empty;

        [JsonPropertyName("liveBroadcastContent")]
        public string LiveBroadcastContent { get; set; } = string.Empty;

        [JsonPropertyName("defaultLanguage")]
        public string DefaultLanguage { get; set; } = string.Empty;

        [JsonPropertyName("localized")]
        public LocalizedDto? Localized { get; set; }

        [JsonPropertyName("defaultAudioLanguage")]
        public string DefaultAudioLanguage { get; set; } = string.Empty;
    }
}
