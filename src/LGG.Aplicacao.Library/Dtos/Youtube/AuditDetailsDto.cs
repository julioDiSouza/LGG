using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class AuditDetailsDto
    {
        [JsonPropertyName("overallGoodStanding")]
        public bool OverallGoodStanding { get; set; }

        [JsonPropertyName("communityGuidelinesGoodStanding")]
        public bool CommunityGuidelinesGoodStanding { get; set; }

        [JsonPropertyName("copyrightStrikesGoodStanding")]
        public bool CopyrightStrikesGoodStanding { get; set; }

        [JsonPropertyName("contentIdClaimsGoodStanding")]
        public bool ContentIdClaimsGoodStanding { get; set; }
    }
}
