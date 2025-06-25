using System.Text.Json.Serialization;

namespace RagAssistant.Share.Models.Services.QdrantCreateCollection
{
    public class QdrantCreateCollectionRequest
    {
        [JsonPropertyName("vectors")]
        public QdrantCreateCollectionItemRequest Vectors { get; set; } = default!;
    }
}
