using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class WatchDto
    {
        [JsonPropertyName("textColor")]
        public string TextColor { get; set; } = string.Empty;

        [JsonPropertyName("backgroundColor")]
        public string BackgroundColor { get; set; } = string.Empty;

        [JsonPropertyName("featuredPlaylistId")]
        public string FeaturedPlaylistId { get; set; } = string.Empty;
    }
}
