using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class ThumbnailsImageDto
    {
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }
}