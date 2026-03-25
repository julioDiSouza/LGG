using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class ImageDto
    {
        [JsonPropertyName("bannerExternalUrl")]
        public string BannerExternalUrl { get; set; } = string.Empty;
    }
}
