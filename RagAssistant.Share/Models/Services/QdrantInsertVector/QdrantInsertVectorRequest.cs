using System.Text.Json.Serialization;

namespace RagAssistant.Share.Models.Services.QdrantInsertVector
{
    public class QdrantInsertVectorRequest
    {
        [JsonPropertyName("points")]
        public List<QdrantInsertVectorItemRequest> Points { get; set; } = default!;
    }
}
