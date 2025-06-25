using System.Text.Json.Serialization;

namespace RagAssistant.Share.Models.Services.QdrantCreateCollection
{
    public class QdrantCreateCollectionItemRequest
    {
        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("distance")]
        public string Distance { get; set; } = default!;
    }
}
