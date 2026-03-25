using System.Text.Json.Serialization;

namespace LGG.Aplicacao.Library.Dtos.Youtube
{
    public class PageInfoDto
    {
        [JsonPropertyName("totalResults")]
        public int TotalResults { get; set; }

        [JsonPropertyName("resultsPerPage")]
        public int ResultsPerPage { get; set; }
    }
}
