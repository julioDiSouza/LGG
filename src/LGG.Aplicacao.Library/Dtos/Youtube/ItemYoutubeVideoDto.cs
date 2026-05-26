using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class ItemYoutubeVideoDto
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; } = string.Empty;

        [JsonPropertyName("etag")]
        public string Etag { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("snippet")]
        public SnippetVideoDto? Snippet { get; set; }

        [JsonPropertyName("statistics")]
        public StatisticsDto? Statistics { get; set; }
    }
}
