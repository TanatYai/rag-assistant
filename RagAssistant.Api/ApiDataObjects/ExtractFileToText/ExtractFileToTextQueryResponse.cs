using System.Text.Json.Serialization;

namespace RagAssistant.Api.ApiDataObjects.ExtractFileToText
{
    public class ExtractFileToTextQueryResponse
    {
        [JsonPropertyName("file_name")]
        public string FileName { get; set; } = default!;

        [JsonPropertyName("text")]
        public string Text { get; set; } = default!;
    }
}
