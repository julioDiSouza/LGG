using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class VideoListResponseDto
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; } = string.Empty;

        [JsonPropertyName("etag")]
        public string Etag { get; set; } = string.Empty;

        [JsonPropertyName("items")]
        public List<ItemYoutubeVideoDto>? Items { get; set; }

        [JsonPropertyName("pageInfo")]
        public PageInfoDto? PageInfo { get; set; }
    }
}
