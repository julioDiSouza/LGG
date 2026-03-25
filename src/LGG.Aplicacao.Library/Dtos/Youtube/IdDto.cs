using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class IdDto
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; } = string.Empty;

        [JsonPropertyName("channelId")]
        public string ChannelId { get; set; } = string.Empty;
    }
}
