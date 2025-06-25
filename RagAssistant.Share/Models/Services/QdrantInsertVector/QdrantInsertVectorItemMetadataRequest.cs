using System.Text.Json.Serialization;

namespace RagAssistant.Share.Models.Services.QdrantInsertVector
{
    public class QdrantInsertVectorItemMetadataRequest
    {
        [JsonPropertyName("chunk_index")]
        public int ChunkIndex { get; set; }

        [JsonPropertyName("file_name")]
        public string FileName { get; set; } = default!;

        [JsonPropertyName("file_extension")]
        public string FileExtension { get; set; } = default!;

        [JsonPropertyName("text")]
        public string Text { get; set; } = default!;
    }
}
