using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class TopicDetailsDto
    {
        [JsonPropertyName("topicCategories")]
        public List<string>? TopicCategories { get; set; }
    }
}
