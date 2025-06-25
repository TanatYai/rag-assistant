using System.Text.Json.Serialization;

namespace RagAssistant.Share.Models.Services.OllamaEmbedding
{
    public class OllamaEmbeddingResponse
    {
        [JsonPropertyName("embedding")]
        public float[] Embedding { get; set; } = [];

        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }
    }
}
