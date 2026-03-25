using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class BrandingSettingsDto
    {
        [JsonPropertyName("channel")]
        public ChannelDto? Channel { get; set; }

        [JsonPropertyName("watch")]
        public WatchDto? Watch { get; set; }

        [JsonPropertyName("image")]
        public ImageDto? Image { get; set; }
    }
}
