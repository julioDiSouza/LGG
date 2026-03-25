using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class ContentDetailsDto
    {
        [JsonPropertyName("relatedPlaylists")]
        public RelatedPlaylistsDto? RelatedPlaylists { get; set; }
    }
}
