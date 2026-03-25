using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class ContentOwnerDetailsDto
    {
        [JsonPropertyName("contentOwner")]
        public string ContentOwner { get; set; } = string.Empty;

        [JsonPropertyName("timeLinked")]
        public DateTime TimeLinked { get; set; }
    }
}
