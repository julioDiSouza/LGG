using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class StatusDto
    {
        [JsonPropertyName("privacyStatus")]
        public string PrivacyStatus { get; set; } = string.Empty;

        [JsonPropertyName("isLinked")]
        public bool IsLinked { get; set; }

        [JsonPropertyName("longUploadsStatus")]
        public string LongUploadsStatus { get; set; } = string.Empty;

        [JsonPropertyName("madeForKids")]
        public bool MadeForKids { get; set; }

        [JsonPropertyName("selfDeclaredMadeForKids")]
        public bool SelfDeclaredMadeForKids { get; set; }
    }
}
