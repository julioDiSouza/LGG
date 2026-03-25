using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class ThumbnailsDto
    {
        [JsonPropertyName("default")]
        public ThumbnailsImageDto? Default { get; set; }

        [JsonPropertyName("medium")]
        public ThumbnailsImageDto? Medium { get; set; }

        [JsonPropertyName("high")]
        public ThumbnailsImageDto? High { get; set; }
    }
}
