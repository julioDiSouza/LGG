using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class ItemChannelDto
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; } = string.Empty;

        [JsonPropertyName("etag")]
        public string Etag { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("snippet")]
        public SnippetDto? Snippet { get; set; }
        
        [JsonPropertyName("contentDetails")]
        public ContentDetailsDto? ContentDetails { get; set; }
        
        [JsonPropertyName("statistics")]
        public StatisticsDto? Statistics { get; set; }
        
        [JsonPropertyName("topicDetails")]
        public TopicDetailsDto? TopicDetails { get; set; }
        
        [JsonPropertyName("status")]
        public StatusDto? Status { get; set; }
        
        [JsonPropertyName("brandingSettings")]
        public BrandingSettingsDto? BrandingSettings { get; set; }
        
        [JsonPropertyName("auditDetails")]
        public AuditDetailsDto? AuditDetails { get; set; }
        
        [JsonPropertyName("contentOwnerDetails")]
        public ContentOwnerDetailsDto? ContentOwnerDetails { get; set; }
        
        [JsonPropertyName("localizations")]
        public LocalizationsDto? Localizations { get; set; }
    }
}
