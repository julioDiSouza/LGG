using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class StatisticsDto
    {
        [JsonPropertyName("viewCount")]
        public string ViewCount { get; set; } = string.Empty;

        [JsonPropertyName("likeCount")]
        public string LikeCount { get; set; } = string.Empty;

        [JsonPropertyName("favoriteCount")]
        public string FavoriteCount { get; set; } = string.Empty;

        [JsonPropertyName("commentCount")]
        public string CommentCount { get; set; } = string.Empty;

        [JsonPropertyName("subscriberCount")]
        public string SubscriberCount { get; set; } = string.Empty;

        [JsonPropertyName("hiddenSubscriberCount")]
        public bool HiddenSubscriberCount { get; set; }

        [JsonPropertyName("videoCount")]
        public string VideoCount { get; set; } = string.Empty;
    }
}
