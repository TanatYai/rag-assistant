using System.Text.Json.Serialization;

namespace RagAssistant.Share.Models.Services.QdrantInsertVector
{
    public class QdrantInsertVectorItemRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = default!;

        [JsonPropertyName("vector")]
        public float[] Vector { get; set; } = default!;

        [JsonPropertyName("payload")]
        public QdrantInsertVectorItemMetadataRequest Payload { get; set; } = default!;
    }
}
