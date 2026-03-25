using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class RelatedPlaylistsDto
    {
        [JsonPropertyName("likes")]
        public string Likes { get; set; } = string.Empty;

        [JsonPropertyName("uploads")]
        public string Uploads { get; set; } = string.Empty;
    }
}
