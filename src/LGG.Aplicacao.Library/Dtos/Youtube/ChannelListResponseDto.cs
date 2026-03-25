using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class ChannelListResponseDto
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; } = string.Empty;

        [JsonPropertyName("etag")]
        public string Etag { get; set; } = string.Empty;

        [JsonPropertyName("pageInfo")]
        public PageInfoDto? PageInfo { get; set; }

        [JsonPropertyName("items")]
        public List<ItemChannelDto>? Items { get; set; }
    }
}