using System.Text.Json.Serialization;

namespace RagAssistant.Share.Models.Services.OllamaEmbedding
{
    public class OllamaEmbeddingRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = default!;

        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = default!;
    }
}
